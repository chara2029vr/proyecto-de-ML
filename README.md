# Zuri - Aterrizaje Forzoso

Proyecto 4 de Lenguajes de Programación: un nivel de un juego de plataformas 2D (scroll lateral) hecho en Unity.

## Historia

Zuri es un pequeño explorador espacial que tuvo un aterrizaje forzoso en el planeta Verdor. Para poder reparar su nave tiene que juntar las monedas de energía y las gemas azules que hay por el camino y llegar a la bandera de rescate que está al final del nivel. En el camino hay pinchos, robots que patrullan, murciélagos y precipicios con agua.

## Controles

| Acción | Tecla |
|---|---|
| Moverse | A / D o flechas izquierda / derecha |
| Saltar | Espacio, W o flecha arriba (si se suelta antes el salto es más bajo) |
| Pausa | Botón de pausa (arriba a la derecha) o ESC |

## Cómo se juega

- **Monedas**: dan 10 puntos cada una.
- **Gemas**: dan 50 puntos cada una.
- El HUD muestra las monedas y gemas recogidas contra el total del nivel (ej: `Monedas: 10/49`) y la puntuación total.
- **Victoria**: tocar la bandera al final del nivel.
- **Derrota**: tocar pinchos, enemigos o caer al agua.

## Escenas

- `MenuPrincipal`: botones Jugar, Créditos, Configuración y Salir.
- `Nivel1`: el nivel. Empieza en pausa con el panel de instrucciones. Tiene HUD, botón de pausa, menú de pausa (Continuar, Configuración, Volver al menú), panel de muerte (Reiniciar) y panel de victoria (Jugar de nuevo, Menú principal).

El nivel está construido con **Tilemaps** (capas Suelo, Pinchos y Decoración) y todas las interacciones usan **Rigidbody2D** y **Colliders 2D**.

## Características adicionales

### 1. Fondo con efecto Parallax

El fondo tiene dos capas (nubes y árboles) que se mueven más lento que la cámara. La capa de nubes se mueve casi igual que la cámara y la de árboles a la mitad, así parece que las nubes están más lejos y se ve profundidad.

Script: `Parallax.cs`. En cada frame calcula la posición del fondo como `inicioX + camara.x * efecto`. Si `efecto` es 0 el fondo se queda quieto como el resto del mundo, si es 1 se mueve pegado a la cámara. Los sprites del fondo usan el modo de dibujo *Tiled* para que se repitan a lo largo de todo el nivel.

### 2. Plataformas móviles

Hay una plataforma que se mueve de lado a lado sobre el lago grande y otra que funciona como elevador para subir al acantilado.

Script: `PlataformaMovil.cs`. La plataforma tiene un Rigidbody2D **Kinematic** y se mueve entre dos puntos (`puntoA` y `puntoB`) con `Vector2.MoveTowards` y `MovePosition` dentro de `FixedUpdate`. Cada frame guarda su velocidad en `velocidadActual`. El jugador revisa si lo que tiene debajo es una plataforma móvil y le suma esa velocidad a la suya, así viaja encima sin resbalarse.

### 3. Récord guardado con PlayerPrefs

Cuando se gana el nivel se compara la puntuación con el récord guardado. Si es mayor se guarda el nuevo récord. El récord se muestra en el panel de victoria y en el menú principal, y no se borra al cerrar el juego.

Código: en `GameManager.Ganar()` se usa `PlayerPrefs.GetInt("Record", 0)` para leer y `PlayerPrefs.SetInt("Record", puntos)` + `PlayerPrefs.Save()` para guardar. En `MenuPrincipal.Start()` se lee para mostrarlo.

### 4. Efectos de sonido y partículas

Al saltar, recoger una moneda, recoger una gema, morir y ganar suena un efecto distinto. Además al recoger una moneda o una gema salen unas partículas del color del objeto.

Código: el jugador tiene un `AudioSource` y usa `PlayOneShot()` con el clip que corresponda. Las partículas son prefabs con un `ParticleSystem` (una ráfaga de 14 partículas, con *Stop Action* en Destroy para que se borren solas) que se crean con `Instantiate()` en la posición del objeto recogido.

## Scripts

| Script | Qué hace |
|---|---|
| `Jugador.cs` | Movimiento, salto, animaciones, recoger objetos, morir |
| `GameManager.cs` | Puntos, contadores del HUD, victoria, derrota y récord |
| `MenuJuego.cs` | Instrucciones, pausa, configuración, reiniciar y volver al menú |
| `MenuPrincipal.cs` | Botones del menú principal |
| `Enemigo.cs` | Enemigos que patrullan entre dos puntos |
| `PlataformaMovil.cs` | Plataformas que se mueven entre dos puntos |
| `CamaraSeguir.cs` | Cámara que sigue al jugador suavemente con límites |
| `Parallax.cs` | Efecto parallax del fondo |

## Créditos

**Integrante:** Maria Luisa Mejia Bermudez

**Assets utilizados:**
- [Pixel Platformer](https://kenney.nl/assets/pixel-platformer) - Kenney (licencia CC0)
- [Kenney Fonts](https://kenney.nl/assets/kenney-fonts) - Kenney (licencia CC0)
- Efectos de sonido hechos para el proyecto

Hecho con Unity 6 (6000.3.17f1).

## Video

Link del video: https://youtu.be/BcwEcQH4mIY
