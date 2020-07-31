export const getUnityVersion = async (): Promise<string> => {
  const { readFile } = await import('pifs')
  const path = await import('path')

  const fileContent = await readFile(path.resolve('ProjectSettings/ProjectVersion.txt'), { encoding: 'utf8' })

  const matches = fileContent.match(/m_EditorVersion: (.+)/)
  const unityVersion = matches?.[1]

  if (typeof unityVersion !== 'string' || unityVersion.length === 0) {
    throw new Error('Cannot get Unity Version')
  }

  return unityVersion
}

export const getUnityLicenseDir = (): string => {
  const activationDir = process.env.UNITY_LICENSE_DIR

  if (typeof activationDir !== 'string' || activationDir.length === 0) {
    throw new Error('Variable "UNITY_LICENSE_DIR" is not set')
  }

  return activationDir
}

export const getUnityLicense = async (): Promise<string> => {
  const unityLicenseEnv = process.env.UNITY_LICENSE

  if (typeof unityLicenseEnv === 'string' && unityLicenseEnv.length > 0) {
    return unityLicenseEnv
  }

  const { readFile } = await import('pifs')
  const unityLicense = await readFile(`${getUnityLicenseDir()}/Unity_v${await getUnityVersion()}.ulf`, { encoding: 'utf8' })

  if (typeof unityLicense !== 'string' || unityLicense.length === 0) {
    throw new Error('Variable "UNITY_LICENSE" is not set')
  }

  return unityLicense
}

export const getUnityUsername = (): string => {
  const username = process.env.UNITY_USERNAME

  if (typeof username !== 'string' || username.length === 0) {
    throw new Error('Variable "UNITY_USERNAME" is not set')
  }

  return username
}

export const getUnityPassword = (): string => {
  const password = process.env.UNITY_PASSWORD

  if (typeof password !== 'string' || password.length === 0) {
    throw new Error('Variable "UNITY_PASSWORD" is not set')
  }

  return password
}

