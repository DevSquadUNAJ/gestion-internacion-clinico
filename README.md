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
Por defecto, todo el código interno (carpetas, namespaces y el .sln) se llama `BaseMicroservicio`. Para cambiar todo esto de forma segura y automática, abre **PowerShell** dentro de la carpeta de tu nuevo repositorio y pega el siguiente script.

**⚠️ IMPORTANTE:** Cambia el valor de `$nuevo` en la línea 3 por el nombre real de tu microservicio (ej. `"Medico"`, `"Enfermeria"`, `"Admision"`).
Ejecuta por bloques. Recuerda cambiar el nombre antes de copiar y pegar para evitar que se ejecute todo de corrido en la consola.

---

$viejo = "BaseMicroservicio"
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
