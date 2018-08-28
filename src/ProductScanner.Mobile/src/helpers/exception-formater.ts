export class ExceptionFormater{    
  public static exceptionMsg(ex: any): string{
    console.warn(ex);
    return typeof (ex.error) !== "string" 
    ? "Something goes wrong. Please try again" 
    : ex.error;
  }
}