import type { THook } from '@auto/core'

const PORT = 4873

export const runNpm: THook = async () => {
  const { default: execa } = await import('execa')
  const { waitForPort } = await import('../utils/wait-for-port')

  // eslint-disable-next-line @typescript-eslint/no-floating-promises
  execa(
    'docker',
    [
      'run',
      '--rm',
      '--name',
      'npm',
      '-p',
      `${PORT}:${PORT}`,
      '-v',
      `${require.resolve('../config/verdaccio.yml')}:/verdaccio/conf/config.yaml`,
      'verdaccio/verdaccio',
    ],
    {
      // stdout: process.stdout,
      stderr: process.stderr,
    }
  )

  await waitForPort(PORT)
}
