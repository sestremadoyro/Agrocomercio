
alter table Operaciones add column opeOrdCompra varchar(20);

--desabilitar primero los triggers de tabla operaciones
update Operaciones set opeOrdCompra = ''