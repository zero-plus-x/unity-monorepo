import plugin from '@start/plugin'

export const publish = () => plugin('publish', ({ logMessage }) => async () => {
  const { auto } = await import('@auto/core')
  const { writePublishTags } = await import('@auto/tag')
  const { makeGithubReleases } = await import('@auto/github')
  const { sendSlackMessage } = await import('@auto/slack')
  const { sendTelegramMessage } = await import('@auto/telegram')
  const { writeChangelogFiles } = await import('@auto/changelog')
  const { concurrentHooks } = await import('../utils/concurrent-hooks')
  const { preparePackages } = await import('../hooks/prepare-packages')
  const { getAutoConfig, getSlackConfig, getGithubConfig, getTelegramConfig } = await import('../utils/publish-helpers')
  const {
    shouldWriteChangelogFiles,
    shouldMakeGitHubReleases,
    shouldSendSlackMessage,
    shouldSendTelegramMessage,
    shouldMakeGitTags,
  } = await getAutoConfig()

  try {
    await auto({
      prePublishCommit: shouldWriteChangelogFiles && writeChangelogFiles,
      prePublish: preparePackages,
      prePush: shouldMakeGitTags && writePublishTags,
      postPush: concurrentHooks(
        shouldMakeGitHubReleases && makeGithubReleases(getGithubConfig()),
        shouldSendSlackMessage && sendSlackMessage(getSlackConfig()),
        shouldSendTelegramMessage && sendTelegramMessage(getTelegramConfig())
      ),
    })
  } catch (e) {
    logMessage(e?.message)

    throw null
  }
})
