import { DocumentType } from '~/enums/document-type'

export const documentTypes = [
  {
    label: 'Transcript of Records',
    value: DocumentType.TOR,
  },
  {
    label: '2nd Copy of Diploma',
    value: DocumentType.SecondCopyOfDiploma,
  },
  {
    value: DocumentType.HonorableDismissal,
    label: 'Honorable Dismissal',
  },
  {
    label: 'Certificate of GPA/GWA',
    value: DocumentType.CertificateOfGPAGWA,
  },
  {
    label: 'Certificate of Enrollment',
    value: DocumentType.CertificateOfEnrollment,
  },
  {
    label: 'Certificate of Medium of Instruction',
    value: DocumentType.CertificateOfMediumOfInstruction,
  },
  {
    label: 'Certificate, Authentication and Verification',
    value: DocumentType.CAV,
  },
  {
    label: 'Authentication',
    value: DocumentType.Authentication,
  },
]
