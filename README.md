1. Introducción

Este proyecto tiene como objetivo el desarrollo de un sistema de gestión académica,
utilizando tecnologías como ASP.NET para el desarrollo web, C# como lenguaje de
programación principal y SSMS para la gestión de la base de datos del proyecto, la integración
de operaciones CRUD sobre estudiantes, cursos y maestros, además de validar datos y manejar
excepciones.

2. Objetivos
   
2.1. Objetivo General

Implementar una solución que permita realizar operaciones CRUD (Crear, Leer,
Actualizar y Eliminar) sobre estudiantes y cursos, gestionando asignaciones, validaciones y
manejo de excepciones. Además, se utilizará una biblioteca de clases para separar la lógica de
negocio y se incorporarán ejercicios prácticos que involucren estructuras de control, ciclos y el
manejo de vectores/matrices.

2.2.Objetivos Específicos

- Crear una aplicación web utilizando ASP.NET core para la gestión de nuestra base de
datos.
- Crear una base de datos en SSMS que almacenará todos los datos que se registren en la
aplicación web.
- Diseñar la GUI de la aplicación web, haciéndola atractiva e intuitiva para los usuarios a
los que se proyecta esta aplicación.

3. Marco Teórico

El presente proyecto tiene como objetivo el desarrollo de aplicaciones orientadas al
manejo de información en entornos académicos, donde la eficiencia y precisión en la gestión de
datos de estudiantes y cursos son fundamentales. El Sistema de Gestión Académica representa
una solución práctica, integrando los conceptos fundamentales de POO, validaciones, control de
flujo y persistencia de datos.

3.1.Programación Orientada a Objetos en .NET
Es el paradigma central en C# y .NET ya que permite estructurar aplicaciones en torno a
clases y objetos, facilitando el mantenimiento, reutilización y escalabilidad del código. En esta
aplicación se implementan clases como Estudiante y Curso, así como clases para gestionar el
programa como EstudianteManager, encargadas de encapsular la lógica de negocio.

3.2.Arquitectura por capas

Capa de presentación: 
Es la responsable de la interfaz con la que el usuario interactúa.
Ya que será una aplicación web, debe ser atractiva y fácil de manejar y navegar.
Capa de Lógica de Negocio: Aquí se encuentra toda la programación que hay detrás de
la aplicación web, como está estructurado nuestro programa, sus clases, relaciones, validaciones
y operaciones CRUD sobre las entidades que manejamos.
Capa de Acceso de Datos: Es la encargada de la interacción y conexión con la base de
datos en SSMS, utilizando un servidor local.

3.3.Operaciones CRUD
Las operaciones CRUD son esenciales en cualquier sistema de gestión, permitiendo
manipular los datos de manera eficiente por los usuarios.

3.4.Validaciones y manejo de excepciones
La validación de datos es un componente esencial en el desarrollo de aplicaciones.
Al utilizar estructuras try-catch, permite controlar errores en tiempo de ejecución, como
errores de conexión a la base de datos o formatos inválidos, garantizando una navegación fluida
y confiable.

3.5.Estructuras de Control, ciclos y colecciones
Las estructuras de control y ciclos repetitivos son esenciales en la implementación de la
lógica del sistema. Permiten validar condiciones, recorrer listas de estudiantes o cursos y aplicar
lógica iterativa como cálculo de promedios, conteos y búsquedas.
Se hace uso de colecciones genéricas como List<Estudiante> o Dictionary<int, Curso>
para almacenar temporalmente la información antes de integrarla con una base de datos.

3.6.Bases de Datos y SQL
Una base de datos es un sistema organizado para almacenar, gestionar y recuperar
información de forma eficiente. En el contexto de aplicaciones académicas como el presente
proyecto. Las bases de datos permiten guardar y consultar datos importantes como estudiantes,
cursos y docentes almacenados en la aplicación web.

SQL
Es el lenguaje estándar que se utiliza para comunicarse con bases de datos relacionales.
Permite realizar operaciones como:
- Crear y modificar tablas.
- Insertar, consultar, actualizar y eliminar datos.
- Definir relaciones entre entidades (como estudiantes y cursos).
- Realizar búsquedas y generar reportes filtrados.

3.7.ASP.NET
Es un framework de desarrollo web creado por Microsoft que permite construir
aplicaciones dinámicas, escalables y seguras sobre la plataforma .NET. Utiliza principalmente el
lenguaje C# y se ejecuta en servidores web como IIS.

Características:
• Interacción con HTML y JavaScript: Permite integrar fácilmente código html,
css y js para construir interfaces atractivas.
• Controles web: Dispone de controles como cajas de texto o botones que
simplifican la creación de entornos gráficos interactivos.
• Validaciones: Incorpora validaciones automáticas para asegurar la correcta
entrada de datos.
• Seguridad: Proporciona autenticación y autorización para controlar el acceso a
partes específicas del sitio.

Ventajas:
• Desarrollo rápido con herramientas visuales.
• Alto rendimiento y soporte para múltiples tecnologías.
• Integración con otras plataformas de Microsoft, como SQL Server, Azure, Office.
• Soporte para desarrollo en la nube y servicios web.

3.8.Herramientas Empleadas
- Lenguaje de programación: C#
- Entorno de desarrollo: Visual Studio 2022
- Aplicación Web con ASP.NET core
- Inteligencia Artificial

4. Desarrollo
   
4.1.Modelado

Para realizar el proceso de modelado lo primero que se hizo fue detallar las clases que se
usarían, para lo cual se definieron las siguientes:
• Carrera
• Docente
• Estudiante
• Materia
• Matricula
• Nivel
• Nota
Cada clase comparte una función específica dentro del programa, pero cada una de ellas
tienen las mismas anotaciones con acciones diferentes. A continuación, se muestra el análisis de
las clases y una breve explicación de sus funciones

Modelo Carrera:
Este modelo contiene la propiedad Nombre, que se encuentra con una validación para
asegurar que se ingrese un valor obligatorio y que dicho valor no exceda los 100 caracteres de
longitud. También se establece una relación uno a muchos con las entidades Estudiante y Nivel,
lo cual permite que una carrera pueda contener varios estudiantes y niveles.

Modelo Docente:
El modelo Docente representa a los profesores dentro del sistema de gestión de
estudiantes. Contiene atributos clave y validaciones, lo que garantiza integridad en la
información ingresada. Cada docente tiene un identificador único con el nombre IdDocente
marcado con la anotación [Key], y campos obligatorios como Cedula, Nombre, Apellido y Correo,
todos ellos validados por formato y longitud. También se establece una relación uno a
muchos con la Materia, esto indica que un docente puede estar asociado a varias materias.

Modelo estudiante:
En la clase Estudiante se definieron las propiedades necesarias para almacenar la
información personal de los estudiantes, incluyendo validaciones para garantizar que los datos
sean ingresados correctamente. Se establecieron relaciones con otras entidades del sistema como
Carrera, la cual tiene una relación de uno a uno y Matricula con una relación de uno a muchos.
El atributo [Required] nos permite validar los campos obligatorios, mientras que [StringLength]
y [EmailAddress] aseguran restricciones de formato y longitud. También se utilizó la anotación
[ForeignKey] para declarar la relación entre el estudiante y la carrera a la que pertenece.

Modelo Materias:
Cada instancia posee un identificador único IdMateria y un nombre, y ambos poseen
validaciones para asegurar su integridad. La clase establece relaciones con las entidades Docente
y Nivel mediante claves foráneas, esto permite organizar las materias según el profesor y el nivel
al pertenecen. La propiedad Matriculas representa una relación uno a muchos, lo cual indica que
una materia puede tener varios estudiantes matriculados.

Modelo Matricula:
El modelo Matricula representa la inscripción de un estudiante en una materia. Este
modelo contiene un identificador único llamado idMatricula y un campo Fecha, que se inicializa
automáticamente con la fecha actual. Esta clase establece relación con las entidades Estudiante y
Materia a través de claves foráneas, esto permite registrar quién se matricula y en qué asignatura.
Se relaciona con la entidad Nota e indica que cada matrícula puede tener varias calificaciones
asociadas. Este modelo es fundamental para llevar el control de inscripciones y seguimiento
académico en el sistema.

Modelo Nivel
El modelo Nivel hace referencia a un nivel académico dentro del sistema de gestión de
estudiantes y está compuesto por una llave primaria llamada IdNivel, un nombre obligatorio con
un límite de 50 caracteres y una relación con la entidad Carrera mediante una clave foránea
llamada IdCarrera. También establece una relación uno a muchos con la entidad Materia, lo cual
indica que un nivel puede tener varias materias asociadas.

Modelo Nota
El modelo Nota se utilizará para registrar calificaciones asociadas a una matrícula en
específico. Este modelo contiene un identificador llamado IdNota, un campo tipo con un límite
de 20 caracteres, y el campo calificación, la cual debe estar dentro del rango máximo de 100
caracteres. Este modelo esta relacionado con la entidad Matricula mediante con ayuda de una
clave foránea llamada IdMatricula, permitiendo vincular cada nota con su matrícula.

4.2.Prototipado en Balsamiq

El prototipo del programa fue desarrollado en la herramienta web “Balsamiq”, la cual
representa el diseño de una interfaz web para el sistema de gestión académica de la Universidad
Técnica de Ambato. Este prototipo tiene como propósito principal modelar de manera visual.

El prototipo mantiene una estructura constante en todas las vistas, lo cual garantiza una
experiencia de usuario consistente. En la parte superior de cada pantalla se incluye una barra de
navegación que simula una barra de direcciones web, acompañada de un conjunto de iconos y
enlaces que permiten acceder rápidamente a las secciones principales del sistema: Inicio,
Estudiantes, Docentes, Materias, Matrículas y Notas. Este enfoque facilita la orientación del
usuario y permite un acceso intuitivo a las funcionalidades del sistema.
La interfaz de gestión de estudiantes, docentes y matriculas presentan una tabla central en
la que se visualiza la información de los datos registrados. Dicha tabla incluye información
acorde a cada sección. Además, se incorpora un botón destacado para registrar nuevos
estudiantes, docentes, matriculas, etc.
Por otro lado, las pantallas de registros incluyen formularios simples y bien estructurados,
conformados por campos para ingresar información acorde a cada campo. Estos formularios
cuentan con dos botones funcionales: uno de color amarillo para cancelar la operación y otro de
color azul para confirmar el registro.
En general el prototipo aplica principios de usabilidad como la consistencia visual y
accesibilidad. Aunque se trata de un prototipo de baja fidelidad, este mismo permite realizar
pruebas tempranas de interacción y detectar posibles mejoras antes del desarrollo definitivo.
A continuación, se muestran imágenes referenciales del diseño realizado:

4.3.Diseño de la Base de Datos

La base de datos del sistema fue desarrollada gracias al sistema SQL Server Management
Studio (SSMS). Desde esta herramienta se definieron las tablas principales que representan los
distintos elementos del sistema, como los estudiantes, docentes, materias, niveles, notas,
matrículas, carreras y sus atributos.
Cada tabla fue diseñada con sus respectivas columnas, definiendo de manera clara los
tipos de datos, claves primarias, claves foráneas y todo tipo de restricciones necesarias. Por
ejemplo, la tabla Estudiante se relaciona con la tabla Persona, ya que un estudiante es una
persona. Lo mismo ocurre con la tabla Nota, la misma que está relacionada con la tabla
Matricula, y esta es usada para registrar las calificaciones de cada estudiante en materias en
específico.
Las relaciones entre tablas se establecieron mediante claves foráneas, lo que permite
mantener la integridad referencial y asegurar que los datos estén interconectados correctamente.
También se usaron índices y restricciones de validación para asegurar un buen funcionamiento y
consistencia de la información en la base de datos.

4.4.Conexión a la Base de Datos

Para conseguir establecer conexión entre la base de datos de SQL Server y el proyecto
ASP.NET Core en Visual Studio 2022, se siguieron pasos fundamentales que nos permitieron
integrar el sistema con la base de datos de manera funcional y correcta.
Dentro del proyecto se configuró el archivo appsettings.json, en donde se declaró la
cadena de conexión la cual contiene los datos necesarios para poder acceder a la base de datos,
como el nombre del servidor, el nombre de la base de datos y el tipo de autenticación.
Después en el archivo Program.cs, se registró el DbContext para que ASP.NET Core
pueda utilizar Entity Framework Core y comunicarse con la base de datos. Esto se realizó
mediante la inyección de dependencias, con el siguiente código:
El DbContext es una clase que actúa como un puente entre el proyecto y la base de datos.
En ella se definen propiedades DbSet<T> que representan las tablas del sistema, como por
ejemplo Estudiantes, Materias, Carreras, etc.
Y gracias a esta configuración, se logra que el proyecto pueda acceder, consultar, insertar,
modificar y eliminar datos directamente desde el entorno de Visual Studio, manteniendo la base
de datos sincronizada con el sistema.

El diseño de la interfaz de usuario se enfocó principalmente en ofrecer una experiencia
clara, sencilla e intuitiva para los usuarios, permitiendo a los mismos interactuar fácilmente con
las todas funcionalidades del sistema. Para lo cual se utilizaron herramientas proporcionadas por
ASP.NET Core junto con HTML, lo cual permitió estructurar las vistas y formularios de manera
clara, sencilla y coherente.
La interfaz está dividida en varias secciones fundamentales que corresponden a los
distintos módulos principales del sistema como gestión de estudiantes, materias, docentes,
matrículas y notas. Cada una de estas secciones presenta su propio formulario para el ingreso,
modificación, búsqueda y eliminación de registros, todo esto siguiendo un modelo visual
uniforme y sencillo.
También se utilizaron controles web como cuadros de texto (textBox), botones(buttons),
listas desplegables y validaciones que guían al usuario durante el ingreso de información.
Además, se incorporaron mensajes de alerta y confirmación para brindar una guía clara sobre las
acciones realizadas, como el registro exitoso o la detección de errores en los datos ingresados.
La navegación del sistema se diseñó mediante un menú principal que permite acceder
rápidamente a las diferentes funcionalidades del sistema, esto para facilitar el uso del sistema
tanto para administradores como para usuarios comunes. Todo el diseño fue realizado con un
enfoque en la simplicidad y la eficiencia, tratando que la interfaz se mantenga limpia y funcional.

5. Resultados Obtenidos
   
Durante el desarrollo e implementación del sistema de gestión académica, se lograron los
siguientes resultados:

- Se desarrollaron formularios web con ASP.NET para el registro, consulta, modificación y
eliminación de estudiantes y cursos.
- Se estructuró el proyecto en capas, presentación, lógica de negocio y acceso a los datos.
- Se aplicaron validaciones tanto para el cliente como para el servidor y control sobre los
diferentes errores de conexión a la base de datos.
- Se desarrollaron vistas web con una interfaz amigable y validaciones visuales y
concretas.
- Se utilizó SQL Server para gestionar la base de datos, implementando tablas relacionales
con claves primarias y foráneas.
- Se aplicaron estructuras condicionales y ciclos repetitivos para la lógica de
procesamiento y validación de datos.

5.1.Discusión de Resultados

El desarrollo del proyecto demostró la correcta aplicación de conceptos clave de
programación, incluyendo estructuras de control, manejo de errores y separación por capas.
La implementación de operaciones CRUD para estudiantes y cursos fue exitosa, y la
lógica de negocio se estructuró adecuadamente mediante una biblioteca de clases, facilitando la
reutilización y el mantenimiento del código.
La integración con SQL Server a través de Entity Framework permitió una gestión
eficiente de la base de datos, cumpliendo con los requerimientos funcionales y no funcionales.
Las validaciones implementadas aseguraron la integridad de los datos y una buena
experiencia de usuario.

6. Conclusiones

- Se implementaron correctamente las funciones CRUD y la asignación de cursos a
estudiantes, con validaciones y manejo de excepciones. La lógica de negocio se organizó
en una biblioteca de clases, usando estructuras y ciclos para mejorar la funcionalidad.
- Se creó una aplicación web funcional con ASP.NET Core, permitiendo gestionar los datos
de forma ordenada y con una estructura escalable.
- Se diseñó una base de datos en SSMS que almacena y gestiona de forma eficiente la
información de la aplicación, relacionada mediante las librerías de Entity Framework.
- Se desarrolló una interfaz web sencilla y atractiva, facilitando la utilización del sistema
para los usuarios finales y mejorando la experiencia del usuario.

7. Recomendaciones

- Incluir un diseño responsivo para dispositivos móviles y mejorar la accesibilidad visual
con estilos modernos.
- Añadir reportes adicionales, como gestión de notas, reportes académicos y control de
asistencia para mejorar el alcance del sistema.
- Implementar respaldos en SQL para proteger la información ante posibles fallos del
sistema.
- Ofrecer instrucciones básicas para que los usuarios puedan usar el sistema con confianza
y sin errores.

8. Referencias Bibliográficas
   
- García, A. E. (s.f.). Manual práctico de SQL. Academia.edu. Recuperado de
https://www.academia.edu/29567267/Manual_Practico_SQL
Academia+6Academia+6Academia+6
- Microsoft. (n.d.). ASP.NET documentation. Microsoft Docs.
https://learn.microsoft.com/en-us/aspnet/core/
9. Anexos
  
Diseño en Balsamiq: https://balsamiq.cloud/sc1kv56/pvf1gec
Repositorio Github:  https://github.com/Isra1322/GestionEstudiantesWebGrupo8
