import { isValidString } from './is-valid-string'

export type TLfsConfig = {
  accessKeyId: string,
  secretAccessKey: string,
  region: string,
  bucketName: string,
  maxCacheSize: string,
  cachePath: string,
}

export const getLfsConfig = (): TLfsConfig => {
  const accessKeyId = process.env.AWS_ACCESS_KEY_ID
  const secretAccessKey = process.env.AWS_SECRET_ACCESS_KEY
  const region = process.env.AWS_DEFAULT_REGION
  const bucketName = process.env.LFS_S3_BUCKET
  const maxCacheSize = process.env.LFS_MAX_CACHE_SIZE || '10GB'
  const cachePath = process.env.LFS_CACHE_PATH || '~/.lfs-cache'

  if (!isValidString(accessKeyId)) {
    throw new Error('Environment variable "AWS_ACCESS_KEY_ID" is not set')
  }

  if (!isValidString(secretAccessKey)) {
    throw new Error('Environment variable "AWS_SECRET_ACCESS_KEY" is not set')
  }

  if (!isValidString(region)) {
    throw new Error('Environment variable "AWS_DEFAULT_REGION" is not set')
  }

  if (!isValidString(bucketName)) {
    throw new Error('Environment variable "LFS_S3_BUCKET" is not set')
  }

  return {
    accessKeyId,
    secretAccessKey,
    region,
    bucketName,
    maxCacheSize,
    cachePath,
  }
}
