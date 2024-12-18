export function formatDate(date: string) {
  if (!date)
    return
  const d = new Date(date)
  return d.toLocaleDateString()
}
export function formatName(row: any) {
  return `${row.lastName}, ${row.firstName} ${row.middleName} ${row.extensionName}`
}
