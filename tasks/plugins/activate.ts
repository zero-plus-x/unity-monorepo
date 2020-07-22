import sequence from '@start/plugin-sequence'
import { activateLicense } from './activate-license'
import { activateRequest } from './activate-request'

export const activate = () => sequence(
  activateRequest(),
  activateLicense()
)
