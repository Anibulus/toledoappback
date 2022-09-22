namespace Toledo.Api.Errors;

public static class Exceptions
{
    public static readonly string ID_NOT_FOUND = "ID no encontrado";
    public static readonly string ELEMENT_CANNOT_BE_NULL = " elemento no puede estar vacío";
    public static readonly string DNI_DUPLICATED = "Documento ya existe";
    public static readonly string USER_NOT_FOUND = "Usuario no encontrado";
    public static readonly string INCORRECT_CREDENTIALS = "Credenciales incorrectas";
    public static readonly string NOT_PERMISSION = "Usted no tiene permiso parra realizar esa acción";
    public static readonly string USER_IS_NOT_ACTIVE = "Este usuario no esta activo";
}
