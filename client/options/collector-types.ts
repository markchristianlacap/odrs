import { CollectorType } from '~/enums/collector-type'

export const collectorTypes = [
  {
    label: 'Myself',
    value: CollectorType.Myself,
  },
  {
    label: 'Immediate Family Member',
    value: CollectorType.ImmediateFamilyMember,
  },
  {
    label: 'Authorized Representative',
    value: CollectorType.AuthorizedRepresentative,
  },
]
