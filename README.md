# 🚀 Plantilla Base - Microservicios .NET 8 (Clean Architecture)

Bienvenido a la plantilla oficial del equipo para la creación de Microservicios Backend.
Esta plantilla ya incluye la separación en 4 capas, referencias correctas y paquetes NuGet base. Sigue estos pasos para crear tu microservicio sin romper la arquitectura.

## 🛠️ Paso 1: Crear tu repositorio desde la plantilla
1. Sube al inicio de esta página en GitHub.
2. Haz clic en el botón verde **"Use this template"** y luego selecciona **"Create a new repository"**.
3. Ponle el nombre de tu microservicio (ej. `microservicio-medico`) y créalo en la organización.

## 💻 Paso 2: Clonarlo en tu PC
Abre tu terminal y clona TU nuevo repositorio (no esta plantilla genérica):

git clone [https://github.com/DevSquadUNAJ/TU_NUEVO_REPO.git](https://github.com/DevSquadUNAJ/TU_NUEVO_REPO.git)
cd TU_NUEVO_REPO

## 🪄 Paso 3: Script de Inicialización del Microservicio
Por defecto, todo el código interno (carpetas, namespaces y el .sln) se llama `Clinico`. Para cambiar todo esto de forma segura y automática, abre **PowerShell** dentro de la carpeta de tu nuevo repositorio y pega el siguiente script.

**⚠️ IMPORTANTE:** Cambia el valor de `$nuevo` en la línea 3 por el nombre real de tu microservicio (ej. `"Medico"`, `"Enfermeria"`, `"Admision"`).
Ejecuta por bloques. Recuerda cambiar el nombre antes de copiar y pegar para evitar que se ejecute todo de corrido en la consola.

---

$viejo = "Clinico"
$nuevo = "PonNuevoNombreAquiMicroservicio"

Write-Host "Iniciando reemplazo de texto de '$viejo' a '$nuevo'..."

Get-ChildItem -Path . -Recurse -File | Where-Object { $_.FullName -notmatch "\\.git\\" } | ForEach-Object {
    $contenido = Get-Content $_.FullName -Raw
    if ($contenido -match $viejo) {
        $contenido -replace $viejo, $nuevo | Set-Content $_.FullName -NoNewline
    }
}

---

Get-ChildItem -Path . -Recurse -File | Where-Object { $_.Name -match $viejo } | Rename-Item -NewName { $_.Name -replace $viejo, $nuevo }

---

Get-ChildItem -Path . -Recurse -Directory | Where-Object { $_.Name -match $viejo } | Sort-Object -Property @{Expression={$_.FullName.Length}; Descending=$true} | Rename-Item -NewName { $_.Name -replace $viejo, $nuevo }

---
## ✅ Paso 4: Verificar y Subir
1. Abre el nuevo archivo `.sln` en Visual Studio.
2. Presiona `Ctrl + Shift + B` para compilar. Todo debería compilar perfectamente en verde.
3. Sube este nuevo estado limpio a tu repositorio para que quede guardado:
git add .
git commit -m "chore: renombrado de plantilla a proyecto real"
git push origin main


--------------------------------------------------------------------------
Para mejorar tu `README.md`, es importante estructurarlo con una jerarquía clara, usar bloques de código para los comandos y resaltar las advertencias de seguridad. Aquí tienes una versión optimizada y profesional:

---

# 🤖 Validador Clínico IA — Guía de Configuración

El **CU11 (Prescribir Tratamiento)** integra un validador de IA que analiza el contexto clínico del paciente para advertir sobre:

* Alergias y contraindicaciones.
* Interacciones medicamentosas.
* Dosis inadecuadas.

La arquitectura utiliza la interfaz `IValidadorClinicoIA`, permitiendo intercambiar el proveedor de IA modificando únicamente una configuración.

---

## 🎛️ Proveedores Disponibles

* **Mock**: Determinístico, sin costo ni internet.
* **Groq**: Ultra-rápido, alta disponibilidad.
* **Gemini**: Modelo alternativo de Google.

---

## 🔧 Configuración General

El proveedor activo se define mediante la clave `IA:Proveedor`. La configuración sigue un orden de prioridad (el último pisa al anterior):

1. `appsettings.json` (Base)
2. `appsettings.Development.json`
3. **`User Secrets`** (Recomendado para desarrollo local)
4. **`Variables de Entorno`** (Recomendado para despliegues)

### Estructura de Prioridad

> [!NOTE]
> Cualquier valor definido en *User Secrets* o *Variables de Entorno* prevalecerá sobre el archivo `appsettings.json`.

---

## 🟦 Opción 1: Validador Mock (Default)

Ideal para desarrollo y testing offline. No requiere configuración adicional.

* **Setup**: En `appsettings.json` o `User Secrets`:
```json
{ "IA": { "Proveedor": "Mock" } }

```


* **Verificación al arrancar**:
```text
[IA] Proveedor seleccionado: Mock
[IA] Usando validador Mock determinístico.

```



---

## 🟢 Opción 2: Groq (Recomendado)

Ofrece un *free tier* generoso y alta velocidad.

### Pasos:

1. **Obtener API Key**: Crea una cuenta en [Groq Console](https://console.groq.com/keys).
2. **Configurar localmente**:
```bash
cd Clinico.API
dotnet user-secrets init
dotnet user-secrets set "IA:Proveedor" "Groq"
dotnet user-secrets set "IA:Groq:ApiKey" "gsk_tu_key_aqui"

```


3. **Verificación**: Al arrancar la app, busca los logs:
```text
[IA] Proveedor seleccionado: Groq
[IA] Modelo Groq: llama-3.3-70b-versatile
[IA] API Key Groq cargada: gsk_xx...XXXX

```



> [!WARNING]
> **Seguridad**: La API Key es personal. **NUNCA** la incluyas (commit) en el repositorio. Usa siempre `User Secrets`.

---

## 🟡 Opción 3: Gemini

Proveedor alternativo de Google.

### Pasos:

1. **Obtener API Key**: Genera tu clave en [Google AI Studio](https://aistudio.google.com/apikey).
2. **Configurar localmente**:
```bash
cd Clinico.API
dotnet user-secrets set "IA:Proveedor" "Gemini"
dotnet user-secrets set "IA:Gemini:ApiKey" "AQ_tu_key_aqui"

```



### Notas sobre Gemini:

* **Modelo**: Se recomienda usar `gemini-flash-latest`.
* **Errores 503**: Indica alta demanda temporal de Google; reintenta en unos segundos.
* **Errores 429**: Verifica que el modelo seleccionado tenga acceso al *free tier* en tu cuenta.

---

## 🚀 Requisitos para ejecutar

Para probar el validador, asegúrate de tener:

1. Los **3 microservicios** levantados.
2. La **Base de Datos** conectada y activa.
3. Tu **API Key** configurada correctamente en el entorno local.

---
