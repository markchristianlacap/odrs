import { DocumentType } from '~/enums/document-type'

export const documentTypes = [
  {
    label: 'Transcript of Records',
    value: DocumentType.TOR,
  },
  {
    label: 'Diploma',
    value: DocumentType.Diploma,
  },
  {
    value: DocumentType.HonorableDismissal,
    label: 'Honorable Dismissal',
  },
  {
    label: 'Certificate of Graduation',
    value: DocumentType.CertificateOfGraduation,
  },
]
