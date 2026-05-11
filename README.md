# Gestion-Salones-Barberias

Este es nuestro proyecto para la administración de salones de belleza y barberías. El sistema está diseñado para facilitar el trabajo diario, permitiendo gestionar las citas de los clientes, controlar el inventario de productos (como shampoo y tintes) y automatizar el cálculo de las comisiones para los estilistas y barberos al final de cada semana.

### Estructura del Proyecto
Para mantener el código organizado, estamos trabajando con una estructura de capas:
* **Models:** Contiene las clases principales (Cliente, Empleado, Servicio).
* **Data:** Aquí se maneja la conexión y persistencia con la base de datos.
* **Services:** Contiene la lógica del negocio, especialmente el cálculo de comisiones basado en los servicios realizados.
* **UI:** Aquí se encuentran los formularios y la interfaz con la que interactúa el usuario.

### Base de Datos
El sistema utiliza una base de datos con tablas para Clientes, Empleados, Servicios y Citas. Los servicios ya incluyen un porcentaje de comisión definido (por ejemplo, 40% para cortes y 50% para tintes). El script con toda la estructura y los datos iniciales está en la carpeta `/database`.
