import path from 'path'

export const getPublishDir = (packageDir: string) => {
  const relativeDir = path.relative(path.resolve('Packages'), packageDir)

  return path.resolve('Publish', relativeDir)
}
