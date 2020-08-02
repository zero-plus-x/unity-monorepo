import plugin from '@start/plugin'

export const activateLicense = () => plugin('activate-license', () => async () => {
  const {
    getUnityUsername,
    getUnityPassword,
    getUnityVersion,
    getUnityLicenseDir,
  } = await import('../utils/unity-helpers')

  // Validate early
  const username = getUnityUsername()
  const password = getUnityPassword()
  const licenseDir = getUnityLicenseDir()
  const unityVersion = await getUnityVersion()

  const { default: execa } = await import('execa')
  const path = await import('path')

  await execa(
    'docker',
    [
      'run',
      '--rm',
      '--name',
      'activate-license',
      '-e',
      `UNITY_USERNAME=${username}`,
      '-e',
      `UNITY_PASSWORD=${password}`,
      '-e',
      `UNITY_ACTIVATION_FILE=${path.join(licenseDir, `Unity_v${unityVersion}.alf`)}`,
      '-e',
      `UNITY_LICENSE_FILE=${path.join(licenseDir, `Unity_v${unityVersion}.ulf`)}`,
      '-v',
      `${path.resolve(licenseDir)}:/app/${licenseDir}`,
      'gableroux/unity3d-activator',
      'node',
      'index.js',
    ],
    {
      stdout: process.stdout,
      stderr: process.stderr,
    }
  )
})
