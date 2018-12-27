man_face_younger_1 = 0x0000000000000001124000000020000000000000001c00800000000000000000
man_face_young_1   = 0x0000000400000001124000000020000000000000001c00800000000000000000
man_face_middle_1  = 0x0000000800000001124000000020000000000000001c00800000000000000000
man_face_old_1     = 0x0000000d00000001124000000020000000000000001c00800000000000000000
man_face_older_1   = 0x0000000fc0000001124000000020000000000000001c00800000000000000000
man_face_younger_2 = 0x000000003f0052064deeffffffffffff00000000001efff90000000000000000
man_face_young_2   = 0x00000003bf0052064deeffffffffffff00000000001efff90000000000000000
man_face_middle_2  = 0x00000007bf0052064deeffffffffffff00000000001efff90000000000000000
man_face_old_2     = 0x0000000bff0052064deeffffffffffff00000000001efff90000000000000000
man_face_older_2   = 0x0000000fff0052064deeffffffffffff00000000001efff90000000000000000

player			   = 0x000000018000000136db6db6db6db6db00000000001db6db0000000000000000

					 0x		|	0000000			|	180000001	|	3|6|d|b|6																		|	6db6db6db6db00000000001		|	d|b|6|d|b																	|	0000000000000000
					 CODE	|	PREAMBLE_28_BIT	|		...		|	EYEBROW_HEIGHT|EYEBROW_POSITION|EYELIDS|(EYE_DEPTH|EYE_SHAPE)|EYE_TO_EYE_DIST	|	...							|	FACE_WIDTH|FACE_RATIO|FACE_DEPTH|TEMPLE_WIDTH|EYEBROW_SHAPE|EYEBROW_DEPTH	|	ENDING_64_BIT

________________________________________________________________
xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx1db6dbxxxxxxxxxxxxxxxx

FACE_WIDTH:
0db
1xx
 c3 --> 1100 0011
 cb --> 1100 1011 !
 d3 --> 1101 0011
 db --> 1101 1011 !
 e3 --> 1110 0011
 eb --> 1110 1011 !
 f3	--> 1111 0011
 fb --> 1111 1011 !

FACE_RATION:
db
dx
 8	--> 	 1000
 9	-->		 1001
 a	-->		 1010
 b	-->		 1011 !
 c	-->		 1100
 d	-->		 1101
 e	-->		 1110
 f	-->		 1111
 
FACE_DEPTH:
6
x
0	-->				000x
2	-->		 		001x
4	-->		 		010x
6	-->		 		011x
8	-->		 		100x
a	-->		 		101x
c	-->		 		110x
e	-->		 		111x

TEMPLE_WIDTH:
6d
6x					xxx0 
 1	-->				xxx0 0001
 5	-->				xxx0 0101
 9	-->				xxx0 1001
 d	-->				xxx0 1101
7x					xxx1
 1	-->				xxx1 0001
 5	-->				xxx1 0101
 9	-->				xxx1 1001
 d	-->				xxx1 1101

EYEBROW_SHAPE:
db
xx
c3	-->					 1100 0011 
cb	-->					 1100 1011 !
d3	-->					 1101 0011 
db	-->					 1101 1011 !
e3	-->					 1110 0011 
eb	-->					 1110 1011 !
f3	-->					 1111 0011 
fb	-->					 1111 1011 !

EYEBROW_DEPTH:
db
dx
 8	--> 	 				  1000
 9	-->		 				  1001
 a	-->		 				  1010
 b	-->		 				  1011 !
 c	-->		 				  1100
 d	-->		 				  1101
 e	-->		 				  1110
 f	-->		 				  1111

________________________________________________________________
xxxxxxxxxxxxxxxx36dbxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

EYEBROW_HEIGHT:
36db
x
0	-->	0000
1	--> 0001
2	--> 0010
3	--> 0011
4	--> 0100
5	--> 0101
6	--> 0110
7	--> 0111

EYEBROW_POSITION:
36db
3x			 !
 0	-->		 0000
 2	--> 	 0010
 4	--> 	 0100
 6	--> 	 0110
 8	-->		 1000
 a	-->		 1010
 c	-->		 1011

EYELIDS:
6db
6xx
 c1	-->			  1100 0001
 c5	--> 		  1100 0101
 c9	--> 		  1100 1001
 cd	--> 		  1100 1101
 d1	--> 		  1101 0001
 d5	--> 		  1101 0101
 d9	--> 		  1101 1001
 dd	--> 		  1101 1101

EYE_DEPTH:
6db
6xx
 c3	-->			  1100 0011 
 cb	-->			  1100 1011 !
 d3	-->			  1101 0011 
 db	-->			  1101 1011 !
 e3	-->			  1110 0011 
 eb	-->			  1110 1011 !
 f3	-->			  1111 0011 
 fb	-->			  1111 1011 !

EYE_SHAPE:
6db
6dx
  8	-->				   1000
  9	-->				   1001
  a	-->				   1010
  b	-->				   1011
  c	-->				   1100
  d	-->				   1101
  e	-->				   1110
  f	-->				   1111

___________________________________________________________________
xxxxxxxxxxxxxxxxxxxx6dbxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

EYE_TO_EYE_DIST:
6db
x
0	-->		 0000
2	-->		 0010
4	-->		 0100
6	-->		 0110
8	-->		 1000
a	-->		 1010
c	-->		 1100
e	-->		 1110

EYE_WIDTH:
6db
6xx
 c1	-->			  1100 0001
 c5	--> 		  1100 0101
 c9	--> 		  1100 1001
 cd	--> 		  1100 1101
 d1	--> 		  1101 0001
 d5	--> 		  1101 0101
 d9	--> 		  1101 1001
 dd	--> 		  1101 1101

CHEEK_BONES:
6db
6xx
 c3	-->			  1100 0011 
 cb	-->			  1100 1011 !
 d3	-->			  1101 0011 
 db	-->			  1101 1011 !
 e3	-->			  1110 0011 
 eb	-->			  1110 1011 !
 f3	-->			  1111 0011 
 fb	-->			  1111 1011 !

NOSE_BRIDGE:
6db
6dx
  8	-->				   1000
  9	-->				   1001
  a	-->				   1010
  b	-->				   1011
  c	-->				   1100
  d	-->				   1101
  e	-->				   1110
  f	-->				   1111

___________________________________________________________________
xxxxxxxxxxxxxxxxxxxxxxx6dbxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

 CONTINUE VIDEO AT 02:47 AND MAKE THE SAME AS BEFORE

___________________________________________________________________
xxxxxxxxxx000000xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

AGE:
000000
xx0000
00
... ?

HAIR_COLOR:
000000
0xx000
 00
 01
 02
 ...?

SKIN:
000000
00x000
  0
  1
  2
  3
  4
  5
  6
  7

BEARD:
00000
000xx
   00
   04
   08
   0c
   10
   14
   18
   1c
   20
   24
   28
   2c
   30
   34
   38
   3c
   40
   44
   48
   4c
   50
   54
   58
   5c
   ... ?

HAIR:
000000
000xx5
   00
   01
   02
   03
   04
   05
   06
   07
   08
   09
   0a
   0b
   0c
   0d
   0e
   0f
   10
   11
   ... ?

