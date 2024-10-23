import { CollectorType } from '~/enums/collector-type'

export const collectorTypes = [
  {
    label: 'Owner',
    value: CollectorType.Owner,
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
