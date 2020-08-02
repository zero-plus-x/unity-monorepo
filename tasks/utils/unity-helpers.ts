import { isValidString } from './is-valid-string'

export const getUnityVersion = async (): Promise<string> => {
  const { readFile } = await import('pifs')
  const path = await import('path')

  const fileContent = await readFile(path.resolve('ProjectSettings/ProjectVersion.txt'), { encoding: 'utf8' })

  const matches = fileContent.match(/m_EditorVersion: (.+)/)
  const unityVersion = matches?.[1]

  if (!isValidString(unityVersion)) {
    throw new Error('Cannot get Unity Version')
  }

  return unityVersion
}

export const getUnityLicenseDir = (): string => {
  const activationDir = process.env.UNITY_LICENSE_DIR

  if (!isValidString(activationDir)) {
    throw new Error('Variable "UNITY_LICENSE_DIR" is not set')
  }

  return activationDir
}

export const getUnityLicense = async (): Promise<string> => {
  const unityLicenseEnv = process.env.UNITY_LICENSE

  if (isValidString(unityLicenseEnv)) {
    return unityLicenseEnv
  }

  const { readFile } = await import('pifs')
  const filePath = `${getUnityLicenseDir()}/Unity_v${await getUnityVersion()}.ulf`
  const unityLicense = await readFile(filePath, { encoding: 'utf8' })

  if (!isValidString(unityLicense)) {
    throw new Error(`File "${filePath}" is empty`)
  }

  return unityLicense
}

export const getUnityUsername = (): string => {
  const username = process.env.UNITY_USERNAME

  if (!isValidString(username)) {
    throw new Error('Variable "UNITY_USERNAME" is not set')
  }

  return username
}

export const getUnityPassword = (): string => {
  const password = process.env.UNITY_PASSWORD

  if (!isValidString(password)) {
    throw new Error('Variable "UNITY_PASSWORD" is not set')
  }

  return password
}

