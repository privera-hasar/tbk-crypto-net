# tbk-crypto-net
Prueba de encriptación para la integración de Transbank QR

## Modo de uso

### Encriptación de datos

El sistema encriptará el valor especificado con la llave pública mostrando el resultado por pantalla.
```
tbk-cripto --encrypt "valor a encriptar"
```
```
tbk-cripto -e "valor a encriptar"
```

### Desencriptación de datos

El sistema desencriptará el valor especificado con la llave pública mostrando el resultado por pantalla.
```
tbk-cripto --decrypt "valor a desencriptar"
```
```
tbk-cripto -d "valor a desencriptar"
```

### Full Test

El sistema intentará encriptar y desencriptar el valor con las llaves pública y privada mostrando los resultados por pantalla.
```
tbk-cripto "valor a encriptar"
```

## Configuración de las llaves
El archivo `keys.json` incluye las llaves pública y privada, mientras que el archivo `public-key.json` solo contiene la llave pública.

Puede generar un par de llaves en el siguiente sitio web:
https://mkjwk.org/
