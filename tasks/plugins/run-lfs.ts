import plugin from '@start/plugin'

const PORT = 8080

export const runLfs = () => plugin('run-lfs', () => async () => {
  const { default: execa } = await import('execa')
  const { waitForPort } = await import('../utils/wait-for-port')
  const { getLfsConfig } = await import('../utils/lfs-helpers')
  const {
    accessKeyId,
    secretAccessKey,
    bucketName,
    region,
    maxCacheSize,
    cachePath,
  } = getLfsConfig()

  // eslint-disable-next-line @typescript-eslint/no-floating-promises
  execa(
    'docker',
    [
      'run',
      '--rm',
      '--name',
      'lfs',
      '-p',
      `${PORT}:${PORT}`,
      '-v',
      `${cachePath}:/data`,
      '-e',
      `AWS_ACCESS_KEY_ID=${accessKeyId}`,
      '-e',
      `AWS_SECRET_ACCESS_KEY=${secretAccessKey}`,
      '-e',
      `AWS_DEFAULT_REGION=${region}`,
      'psxcode/rudolfs',
      '--cache-dir=/data',
      `--s3-bucket=${bucketName}`,
      `--max-cache-size=${maxCacheSize}`,
    ],
    {
      stdout: process.stdout,
      stderr: process.stderr,
    }
  )

  await waitForPort(PORT)
})
