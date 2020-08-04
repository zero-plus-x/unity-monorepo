import plugin from '@start/plugin'

export const publish = () => plugin('publish', ({ logMessage }) => async () => {
  const { auto } = await import('@auto/core')
  const { sendSlackMessage } = await import('@auto/slack')
  const { sendTelegramMessage } = await import('@auto/telegram')
  const { writeChangelogFiles } = await import('@auto/changelog')
  const { createOpenUpmTags } = await import('../hooks/create-openupm-tags')
  const { makeGithubReleases } = await import('../hooks/make-github-release')
  const { preparePackages } = await import('../hooks/prepare-packages')
  const { concurrentHooks } = await import('../utils/concurrent-hooks')
  const { getAutoConfig, getSlackConfig, getGithubConfig, getTelegramConfig } = await import('../utils/publish-helpers')
  const { shouldSendSlackMessage, shouldSendTelegramMessage } = await getAutoConfig()

  try {
    await auto({
      prePublishCommit: writeChangelogFiles,
      prePublish: preparePackages,
      prePush: createOpenUpmTags,
      postPush: concurrentHooks(
        makeGithubReleases(getGithubConfig()),
        shouldSendSlackMessage && sendSlackMessage(getSlackConfig()),
        shouldSendTelegramMessage && sendTelegramMessage(getTelegramConfig())
      ),
    })
  } catch (e) {
    logMessage(e?.message)

    throw null
  }
})
