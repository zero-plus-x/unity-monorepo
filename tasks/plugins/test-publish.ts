import plugin from '@start/plugin'

export const testPublish = () => plugin('test-publish', ({ logMessage }) => async () => {
  const { concurrentHooks } = await import('../utils/concurrent-hooks')
  const { auto } = await import('@auto/core')
  const { preparePackages } = await import('../hooks/prepare-packages')
  const { runNpm } = await import('../hooks/run-npm')
  const { publishPackages } = await import('../hooks/publish-packages')

  try {
    await auto({
      depsCommit: false,
      publishCommit: false,
      prePublish: concurrentHooks(
        preparePackages,
        runNpm
      ),
      publish: publishPackages({
        registry: 'http://localhost:4873',
      }),
      push: false,
    })
  } catch (e) {
    logMessage(e?.message)

    throw null
  }
})
