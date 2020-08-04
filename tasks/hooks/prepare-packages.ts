import path from 'path'
import type { THook } from '@auto/core'
import globby from 'globby'
import { mkdir, copyFile } from 'pifs'
import { getPublishDir } from '../utils/publish-helpers'

const makePusblishDir = async (packageDir: string): Promise<string> => {
  const publishDir = getPublishDir(packageDir)

  await mkdir(publishDir, { recursive: true })

  return publishDir
}

const createFileCopier = async (packageDir: string) => {
  const publishDir = await makePusblishDir(packageDir)

  return async (filePath: string) => {
    const dstPath = path.join(publishDir, path.relative(packageDir, filePath))

    await mkdir(path.dirname(dstPath), { recursive: true })
    await copyFile(filePath, dstPath)
  }
}

const EXCLUDE_FILE_GLOBS = ['!**/*.meta', '!**/Tests/**', '!changelog.md']

const preparePackage = async (packageDir: string) => {
  const paths = await globby([`${packageDir}/**`, ...EXCLUDE_FILE_GLOBS], { absolute: false })
  const copyFile = await createFileCopier(packageDir)

  await Promise.all(
    paths.map(copyFile)
  )
}

export const preparePackages: THook = async ({ packages }) => {
  for (const pkg of packages) {
    if (pkg.type === null || pkg.version === null) {
      continue
    }

    await preparePackage(pkg.dir)
  }
}
