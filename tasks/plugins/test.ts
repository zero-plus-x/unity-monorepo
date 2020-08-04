import plugin from '@start/plugin'

const runTest = async (mode: 'edit' | 'play') => {
  if (mode !== 'edit' && mode !== 'play') {
    throw new Error(`Test mode should be set to "edit" or "play". Got ${mode}.`)
  }

  const { default: execa } = await import('execa')
  const { getUnityLicense, getUnityVersion } = await import('../utils/unity-helpers')

  try {
    const { stdout } = await execa(
      'docker',
      [
        'run',
        '--rm',
        '--name',
        'unity-test',
        '-e',
        `UNITY_LICENSE=${await getUnityLicense()}`,
        '-e',
        `TEST_PLATFORM=${mode}mode`,
        '-v',
        `${process.cwd()}:/project`,
        '-w',
        '/project',
        `gableroux/unity3d:${await getUnityVersion()}`,
        '/bin/bash',
        '-c',
        'tasks/scripts/test.sh',
      ]
    )

    return stdout
  } catch {
    throw new Error('docker run command failed')
  }
}

type TTestMode = 'edit' | 'play'

export const test = (mode?: TTestMode) => plugin('unity-test', ({ logMessage }) => async () => {
  const { transformTestResult, printTestResult, isTestOk } = await import('../utils/test-helpers')

  const modes: TTestMode[] = (mode === 'edit' || mode === 'play') ? [mode] : ['edit', 'play']

  for (const mode of modes) {
    logMessage(`Running tests in ${mode} mode...`)

    const stdout = await runTest(mode)
    const testResults = await transformTestResult(stdout)

    await printTestResult(testResults, console.log)

    if (!isTestOk(testResults)) {
      throw null
    }
  }
})
