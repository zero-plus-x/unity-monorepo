import type { THook } from '@auto/core'

const makePusblishDir = async (packageDir: string): Promise<string> => {
  const { mkdir } = await import('pifs')
  const { getPublishDir } = await import('../utils/get-publish-dir')
  const publishDir = getPublishDir(packageDir)

  await mkdir(publishDir, { recursive: true })

  return publishDir
}

const createFileCopier = async (packageDir: string) => {
  const path = await import('path')
  const { mkdir, copyFile } = await import('pifs')
  const publishDir = await makePusblishDir(packageDir)

  return async (filePath: string) => {
    const dstPath = path.join(publishDir, path.relative(packageDir, filePath))

    await mkdir(path.dirname(dstPath), { recursive: true })
    await copyFile(filePath, dstPath)
  }
}

const EXCLUDE_FILE_GLOBS = ['!**/*.meta', '!**/Tests/**', '!changelog.md']

const preparePackage = async (packageDir: string) => {
  const { default: globby } = await import('globby')
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
