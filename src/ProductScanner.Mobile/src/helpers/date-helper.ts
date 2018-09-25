export function toUploadDateString(source: Date): string {
  const date = new Date(source);
  var delta = Math.abs((date as any) - (new Date() as any)) / 1000;
  if(isNaN(delta)){
    return "";
  }

  // calculate (and subtract) whole days
  var days = Math.floor(delta / 86400);
  if (days > 0) {
    console.log(`uploaded ${days} DAYS ago`);
    return `uploaded ${days} days ago`;
  }

  // calculate (and subtract) whole hours
  var hours = Math.floor(delta / 3600) % 24;
  if (hours > 0) {
    return `uploaded ${hours} hours ago`;
  }

  // calculate (and subtract) whole minutes
  var minutes = Math.floor(delta / 60) % 60;
  if (minutes > 0) {
    return `uploaded ${minutes} minutes ago`;
  }

  var seconds = delta % 60;
  return `uploaded ${seconds} minutes ago`;
}