# tbk-crypto-net
Prueba de encriptación para la integración de Transbank QR utilizando JWE.

Esta aplicación simula la encriptación y desencriptación de las solicitudes HTTP que Hasar hará a la API del QR interoperable de Transbank, como también de las respuestas que Transbank envíe a Hasar.

Como precondición para operar Transbank tendrá que informar su llave pública a Hasar. Con esta aplicación se incluyen llaves ficticias que no corresponden con ninguna llave de Hasar ni de Transbank.

Las solicitudes de Hasar a Transbank se encriptan con la llave pública de Transbank y contienen, en un encabezado protegido (JWE Protected Header), la llave pública con la que debe encriptarse la respuesta. Por su parte, Transbank utilizará su llave privada para desencriptar la solicitud.

Las respuestas de Transbank se encriptan con la llave pública incluída en la solicitud de Hasar, quien desencriptará la respuesta utilizando su llave privada. Estas respuestas también incluyen la llave pública de Hasar, con la que se encriptó la solicitud en los encabezados protegidos, pero no tienen ningún uso por parte de Hasar, ya que se tiene conocimiento de antemano de cuál llave se envió a Transbank para encriptar el mensaje.

Si bien se utiliza la misma llave pública de Transbank para todas las solicitudes de Hasar. Este último podría cambiar la llave pública en cada solicitud si así lo requiere o utilizar la misma llave por un tiempo determinado.

## Modo de uso

### Encriptación de una solicitud de Hasar a Transbank

El sistema encriptará el valor especificado con la llave pública de Transbank, incluyendo la llave pública de Hasar en el protected header app-key mostrando el resultado por pantalla.
```
tbk-cripto --encrypt "hasar-plain-text-request"
```
```
tbk-cripto -e "hasar-plain-text-request"
```

### Desencriptación de una respuesta de Transbank

El sistema desencriptará el token especificado con la llave privada de Hasar mostrando el resultado por pantalla.
```
tbk-cripto --decrypt "tbk-jwe-token-response"
```
```
tbk-cripto -d "tbk-jwe-token-response"
```

### Encriptación de una respuesta de Transbank

El sistema encriptará el valor especificado con la llave pública de Hasar, incluyendo la llave pública de Hasar en el protected header app-key mostrando el resultado por pantalla.
```
tbk-cripto --encrypt-hasar "tbk-plain-text-response"
```
```
tbk-cripto -E "tbk-plain-text-response"
```

### Desencriptación de una solicitud de Hasar a Transbank

El sistema desencriptará el token especificado con la llave privada de Transbank mostrando el resultado por pantalla.
```
tbk-cripto --decrypt-tbk "hasar-jwe-token-request"
```
```
tbk-cripto -D "tbk-jwe-token-request"
```
**Nota:** Este método supone que se conoce la llave privada de Transbank. Se sugiere crear un par de llaves aleatorio para simular las llaves de Transbank o utilizar las que acompañan a la aplicación.

### Full Test

El sistema intentará encriptar y desencriptar el valor con las llaves pública y privada de Hasar y Transbank mostrando los resultados por pantalla.
```
tbk-cripto "plain-text"
```

## Configuración de las llaves
Los siguientes archivos contienen las llaves públicas y privadas.
* El archivo `hasar-keys.json` incluye las llaves pública y privada de Hasar.
* El archivo `hasar-public-key.json` solo contiene la llave pública de Hasar.
* El archivo `tbk-keys.json` incluye las llaves pública y privada de Transbank.
* El archivo `tbk-public-key.json` solo contiene la llave pública de Transbank.

Puede generar un par de llaves en el siguiente sitio web:
https://mkjwk.org/

Key size: 2048
Key use: Encryption
Algorithm: RSA-OAEP-256: RSAES OAEP using SHA-256 and MGF1 with SHA-256
Key ID: SHA-256
Show X.509: Yes, si desea tener también las llaves en formato PEM.
