# Semesterprojekt PE

Von Manuel Geissmann, Valentin Huber und Felix Saaro

Dieses Dokument enthält LaTeX-Formeln. Ein Editor wie VSCode oder Obsidian 
mit den entsprechenden Erweiterungen wird empfohlen.

## Teil 1
- Harmonischer Oszillator (Feder)
	- $\overrightarrow F=-k\cdot \overrightarrow x$
	- Kraft wirkt entgegen der Auslenkung
## Teil 2
- $\overrightarrow F=-k\cdot \overrightarrow x$
- Kraft wirkt entgegen der Auslenkung
- Nach einigen Schwingungen lösen, sobald Koordinate über Ruhepunkt sind
- Würfel soll $1 \text{kg}$ schwer sein und $1 \frac{\text m}{\text s}$ 
erreichen. Die Auslenkung sei $x$.
$$\begin{aligned}
E_\text{kin} &= \frac 1 2 m v^2 \\
E_\text{spann} &= \frac 1 2  k x^2 \\
E_\text{spann} &= E_\text{kin} \\
\frac 1 2  k x^2 &= \frac 1 2 m v^2 \\
k &= \frac{mv^2}{x^2}\\
k &= \frac{1\cdot1^2}{x^2}\\
k &= \frac{1}{x^2}\\
\end{aligned}
$$
## Teil 3a
- Impuls von Würfel $W_n$: $\overrightarrow{p_{W_n}} = m_{W_n}\cdot 
\overrightarrow{v_{W_n}}$
- Gesamtimpuls der Würfel $W_1, W_2, \ldots, W_k$: 
$\overrightarrow{p_\text{Ges}} =\displaystyle\sum_{i=1}^k 
\overrightarrow{p_{W_i}}$
### Feder-Komprimierung
- $W_1$ gleitet am Anfang mit $1\frac m s$ (siehe Teil 2).
- Feder hat Ruhelänge $l_F$
- Feder hat Konstante $k$
- Distanz zwischen $W_1$ und $W_2$ sei $d_W$
- Sobald Würfel 1 Feder berührt, wird dieser mit $\overrightarrow F=-k 
\cdot\overrightarrow x$ gebremst. Dabei ist $\overrightarrow{x} = 
\overrightarrow{l_F} - \overrightarrow{d_W}$
- Da die Feder komprimiert ist, wirkt auch eine Kraft auf $W_2$, diese ist 
gleich gross, aber entgegengesetzt.
- $\left|\overrightarrow{d_W}\right|$ ist am kleinsten wenn 
$\overrightarrow{v_{W_1}} = \overrightarrow{v_{W_2}}$
- Dann wird die Feder «eingefroren»
### Feder-Entlastung
- Würfel gleiten gemeinsam für einige Sekunden
- Solange $\overrightarrow{l_F} > \overrightarrow{d_W}$ wirkt auf $W_1$ 
und $W_2$ wieder die selbe Kraft wie bei der Feder-Komprimierung

## Teil 3b
- Zentripetalkraft $F_Z$, die Körper mit Masse $m$ auf Radius $R$: 
$F_Z=\frac{mv^2}{R}$
- Bei Radius $5m$ ist die Distanz bis zum Stopppunkt $\frac{\pi\cdot r}{2} 
= 2.5\pi m$
- Reibungskraft (in einer Dimension) $F_R = -\mu\cdot F_N\cdot e_v$
- $E_{\text{kin}, W_1} = \frac 1 2 m_{W_1} \cdot v_{W_1}^2$
- $E_{W_1, \text{Ende}} = 0J$
- Reibungsarbeit $W_R = x\cdot F_R$ mit der Anhaltedistanz $x$
$$\begin{aligned}
E_{\text{kin}, W_1, 0}-E_{\text{kin}, W_1, \text{Ende}} &= 
\left|W_R\right| \\
E_{\text{kin}, W_1, 0}-0 &= \left|W_R\right| \\
\frac 1 2 m_{W_1}\cdot v_{W_1, 0}^2 &= \mu\cdot F_N \\
\frac 1 2 \cdot v_{W_1, 0}^2&= \mu \cdot m_{W_1}\cdot 
g\cdot\cos{\left(\alpha\right)} \\
\frac 1 2 \cdot v_{W_1, 0}^2&= \mu \cdot g\cdot\cos{\left(0\right)} \\
\mu &= \frac{v_{W_1, 0}^2}{2\cdot g}
\end{aligned}$$

