import { RequestStatus } from '~/enums/request-status'

export const requestProcess = [
  {
    value: RequestStatus.Submitted,
    label: 'Submitted',
    color: 'info',
  },
  {
    value: RequestStatus.WaitingForPayment,
    label: 'Waiting For Payment',
    color: 'warning',
  },
  {
    value: RequestStatus.PaymentSubmitted,
    label: 'Payment Submitted',
    color: 'positive',
  },
  {
    value: RequestStatus.OnProcess,
    label: 'On Process',
    color: 'info',
  },
  {
    value: RequestStatus.PendingForRelease,
    label: 'Pending For Release',
    color: 'info',
  },
  {
    value: RequestStatus.Released,
    label: 'Released',
    color: 'positive',
  },
]