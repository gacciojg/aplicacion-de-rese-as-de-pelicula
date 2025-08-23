aplicación de reseñas de peliculas/series

funcionamiento:
Login => se ingresa a la aplicación mediante dos cuentas

         usuario: Nombre = "user"
                  contraseña = "abcdefgh"
         
         administrador: Nombre = "admin" 
                        contraseña = "12345678"
         si ingresa otra cuenta que no es, le saltara error

dos caminos: dependiendo de cual cuenta escoga, será el sitio donde terminara una ves de a iniciar sesión, si ingresa la cuenta usuario ira directo a la sección de de reseñas (aclaro que no vera absolutamente nada porque al tener una base de datos de memoria los datos se resetean entonces no habrá ninguna pelicula cargada) y si ingresa como administrador, ira al menú.

Menu => si ingreso como administrador ira directo al menú donde tendrá 4 opciones, 
        "gestión de peliculas", "catalogo de peliculas", "login" y "lista de                 usuarios"   
  
        gestión de peliculas: es la parte donde podrá agregar la pelicula al catalogo, podrá ser capaz de agregar una imagen local 
                              desde alguna carpeta que tenga   almacenadas las imagenes, editarlo, eliminarlo y visualizar la lista 
                              de peliculas agregadas

        catalogo de reseñas: es la sección de reseñas donde visualizara las peliculas cargadas en gestión de peliculas

        login: podrá volver al login
         
        lista de usuarios: (Actualizacion) ahora usted podría visualizar a los usuarios 
                           (en este caso pueden ser usuario o administrador de momento)

catalgo de reseñas (como user) => si ingresa como user va directamente al catalogo, en el que podrá ver, calificar y reseñas la pelicula que 
                                  haya cargado el administrador

        
