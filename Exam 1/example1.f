C==========================================================
C
C This progran computes the value of Pi using numerical
C integration.
C Author: A. Ortiz, 2018.
C
C==========================================================
      program pi
*
      integer numrects, i
      real mid, height, width, area, sum
*
      sum = 0
*
      write(*, *) 'Number of rectangles:'
      read(*, *) numrects
*
      width = 1.0 / numrects
c----------------------------------------------------------
c Start of loop
c
      do 42 i = numrects - 1, 0, -1
          mid = (i + 0.5) * width
          height = 4.0 / (1.0 + mid ** 2)
          sum = sum + height
42    continue
*
* Ending of loop.
*----------------------------------------------------------
      area = width * sum
      write(*, *) 'Computed pi = ', area
      stop
      end
c End of source code.
