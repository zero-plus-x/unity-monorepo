import path from 'path'
import { readFile } from 'pifs'
import { TReadonly } from 'tsfn'

export type TNpmConfig = {
  registry?: string,
  access?: 'restricted' | 'public',
}

export type TPackageJson = {
  name: string,
  version: string,
  publishConfig?: {
    registry?: string,
  },
  auto?: any,
}

const readPackage = async (packageDir: string): Promise<TPackageJson> => {
  const packageJsonPath = path.join(packageDir, 'package.json')
  const packageJsonData = await readFile(packageJsonPath, { encoding: 'utf8' })

  return JSON.parse(packageJsonData)
}

const defaultConfig: TReadonly<Required<TNpmConfig>> = {
  registry: 'https://registry.npmjs.org/',
  access: 'restricted',
}

export const resolveNpmConfig = async (packageDir: string, rootNpmConfig?: TNpmConfig, overrideRegistry?: string): Promise<Required<TNpmConfig>> => {
  const { auto: pkgAutoConfig = {} } = await readPackage(packageDir)

  const resultConfig = {
    ...defaultConfig,
    ...rootNpmConfig,
    ...pkgAutoConfig.npm,
  }

  if (typeof overrideRegistry !== 'undefined') {
    resultConfig.registry = overrideRegistry
  }

  return resultConfig
}
