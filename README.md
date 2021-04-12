# PruebaCsvImporter_KatherineHernandez
## Proceso Ejecucion

1. Descargue las fuentes
2. Compile la solucion.
3. Ejecute los scripts de BD que se encuentran en la ruta: [script](https://github.com/khernandezl/PruebaCsvImporter_KatherineHernandez/DAL/Scripts), es importante aclarar que se debe crear por el localDB, en SQL Server Management
4. Con los pasos anteriores se procede a ajecutar la aplicacion.


## Construccion de la prueba
Tuve varios conflictos con la construccion, ya que aparentemente no se veia tan compleja, pero al revisar la cantidad de registros, me hizo pensar mucho en el performance de la maquina y el tiempo de ejecucion.
En esete caso sacrifique procesamiento por rapidez, pero obtimizando de igual forma los recursos de la maquina.
Se me presentaron varios escenarios en la ejecucion, en alguno tuve demora de horas y casi de dias en procesar la informacion, que me toco reeplantear la forma en la que se estaba construyendo, indaganado encontre una dll que me permitia optimizar recursos y hacer las cosas un poco mas rapidas, la dll CSVHelper, no la conocia la verdad, pero estudiando se aprender y logre implementarla.
Por ultimo realizo un volcado de datos utulizando el BulkInsertAsync, en la insercion tambien tuve inconvenientes por timeout, asi que decidi hacerlo por lotes para que la transaccion no quedara en espera y de esta forma se aligero mas el proceso.
