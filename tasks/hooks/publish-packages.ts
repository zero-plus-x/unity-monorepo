import type { THook, TPackageRelease } from '@auto/core'
import execa from 'execa'
import prompts from 'prompts'
import type { TReadonly } from 'tsfn'
import { isFunction, isString } from 'tsfn'
import { getPublishDir } from '../utils/get-publish-dir'
import { resolveNpmConfig } from '../utils/resolve-npm-config'
import type { TNpmConfig } from '../utils/resolve-npm-config'

const makeRetryPrompt = async (): Promise<void> => {
  await prompts({
    type: 'select',
    name: 'value',
    message: 'Fix the above issue',
    choices: [
      { title: 'Try Again', value: 'YES' },
    ],
    initial: 0,
  })
}

type TPublishPackage = Pick<TPackageRelease, 'name' | 'dir'>

const isNpmAlreadyExistsError = (err: unknown) => isString(err) && err.includes('previously published versions')

const publishPackage = async (packageRelease: TReadonly<TPublishPackage>, npmConfig: TReadonly<Required<TNpmConfig>>, logMessage: (message: string) => void, logError: (err: Error) => void): Promise<void> => {
  const publishDir = getPublishDir(packageRelease.dir)

  console.log(publishDir)

  const invokePublish = () =>
    execa('npm', [
      'publish',
      '--registry',
      npmConfig.registry,
      '--access',
      npmConfig.access,
      publishDir,
    ], {
      stdin: process.stdin,
      stdout: process.stdout,
      stderr: 'pipe',
    })

  let shouldRetry: boolean

  do {
    try {
      await invokePublish()
      shouldRetry = false
    } catch (e) {
      if (isNpmAlreadyExistsError(e.stderr)) {
        logMessage(`Package "${packageRelease.name}" has already been published`)
        shouldRetry = false
      } else {
        logError(e)
        await makeRetryPrompt()
        shouldRetry = true
      }
    }
  } while (shouldRetry)
}

export type TPublishPackageConfig = {
  registry?: string,
  onMessage?: (message: string) => void,
  onError?: (e: Error) => void,
}

export const publishPackages = (publishConfig: TPublishPackageConfig = {}): THook => {
  const logMessage = isFunction(publishConfig.onMessage) ? publishConfig.onMessage : () => {}
  const logError = isFunction(publishConfig.onError) ? publishConfig.onError : () => {}

  return async ({ packages, config }) => {
    for (const pkg of packages) {
      if (pkg.type === null || pkg.version === null) {
        continue
      }

      const npmConfig = await resolveNpmConfig(pkg.dir, config.npm, publishConfig.registry)

      await publishPackage(pkg, npmConfig, logMessage, logError)
    }
  }
}
