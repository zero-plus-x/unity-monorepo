import plugin from '@start/plugin'

export const activateRequest = () => plugin('activate-request', () => async () => {
  const { default: execa } = await import('execa')
  const { default: fs } = await import('pifs')
  const { getUnityVersion, getUnityLicenseDir } = await import('../utils/unity-helpers')
  const path = await import('path')

  const unityVersion = await getUnityVersion()
  const licenseDir = getUnityLicenseDir()

  try {
    await fs.mkdir(licenseDir)
  } catch {}

  await execa(
    'docker',
    [
      'run',
      '--rm',
      '--name',
      'get-unity-activation-file',
      '-e',
      `UNITY_LICENSE_DIR=${licenseDir}`,
      '-v',
      `${path.resolve(licenseDir)}:/project/${licenseDir}`,
      '-v',
      `${path.resolve('tasks/scripts/get_activation_file.sh')}:/project/get_activation_file.sh`,
      '-w',
      '/project',
      `gableroux/unity3d:${unityVersion}`,
      '/bin/bash',
      '-c',
      './get_activation_file.sh',
    ],
    {
      stdout: process.stdout,
      stderr: process.stderr,
    }
  )
})
