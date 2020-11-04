; Finding the shortest path using Dijkstra algorithm

; Algorithm finds the shortest path in a maze

;3.11.2020
; Winter Semester, 2020/2021
; Lukasz Kwiecien Informatics

; gets neighbours of the current element 

; modified registers: 
; xmm0 - contains indexes of the neigbours when the macro is finished
; r12d - is used to calculate the index of the neighbour
get_neighbours macro reg

	pxor xmm0, xmm0 ; reset the xmm0 register

	mov r12d, [current] ; move index of the current element to the r12d register
	mov ecx, 40 ; move value 40 (boundary value of the row) to the ecx register, it is a preparation for calculating current%40 
	CDQ ; convert double to quad
	IDIV ecx ; divide eax by edx, modulo is performed here

	mov r12d, edx ; move edx (result of the modulo operation) to the r12d register
	cmp r12d, 0 ; check if the result is equal to zero - it means current cell is on the left bound
	JE first ; jump to the section calculating indexes of the neighbours when current%40=0

	cmp r12d, 39 ; check if the result is equal to 39 - it means current cell is on the right bound
	JE second ; jump to the section calculating indexes of the neighbours when current%40=39

	; if current % 40 is not equal to 0 or 39 it means that the current cell is not on the left or right bound so we don't have to worry about picking the invalid neighbour (e.g. -1)
	mov r12d, [current] ; move current to r12d register
	add r12d, 1 ; add one to the current index - right side neighbour
	pinsrd xmm0, r12d, 0 ; insert the first neighbour into the xmm0 register
	sub r12d, 2  ; subtract one from the current index - left side neighbour
	pinsrd xmm0, r12d, 1 ; insert the second neighbour into the xmm0 register
	jmp part_2 ; jump to the section calculating the upper and bottom  neighbours

first: ; if current cell is on the right bound 
    mov r12d, [int_max] ; move max value to the r12d register - it will mark it as invalid neighbour
	pinsrd xmm0, r12d, 0; insert the first neighbour into the xmm0 register
	mov r12d, [current] ; move current to r12d register
	add r12d,1  ; add one to the current index - right side neighbour
	pinsrd xmm0, r12d, 1 ; insert the second neighbour into the xmm0 register
	jmp part_2 ; jump to the section calculating the upper and bottom  neighbours

second:  ; if current cell is on the left bound 
    mov r12d, [int_max] ; move max value to the r12d register - it will mark it as invalid neighbour
	pinsrd xmm0, r12d, 0 ; insert the first neighbour into the xmm0 register
	mov r12d, [current]   ; move current to r12d register
	sub r12d,1 ; subtract one from the current index - left side neighbour
	pinsrd xmm0, r12d, 1 ; insert the second neighbour into the xmm0 register
	jmp part_2  ; jump to the section calculating the upper and bottom  neighbours

part_2: ; this section is responsible for getting upper and bottom  neighbour
		mov r12d, [current] ; move current to r12d register
		add r12, 40 ; add 40 to the current index - bottom neighbour
		cmp r12, 800 ; check if the neigbhour is in the range of the array
		JAE third ; if not go to te appropriate section

		mov r12d, [current] ; move current to r12d register
		sub r12, 40 ; subtract 40 from the current index - upper neighbour
		cmp r12, 800 ; check if the neigbhour is in the range of the array
		JAE fourth ; if not go to te appropriate section

		pinsrd xmm0, r12d, 2 ; insert the third neighbour into the xmm0 register
		add r12, 80  ; add 40 to the current index - bottom neighbour
		pinsrd xmm0, r12d, 3 ; insert the fourth neighbour into the xmm0 register
		jmp ret_neighbours ; return neighbours

third: ; if current cell is on the bottom bound 
    mov r12d, [int_max] ; move max value to the r12d register - it will mark it as invalid neighbour
	pinsrd xmm0, r12d, 2 ; insert the third neighbour into the xmm0 register
	mov r12d, [current] ; move current to r12d register
	sub r12d,40 ; subtract 40 from the current index - upper neighbour
	pinsrd xmm0, r12d, 3 ; insert the fourth neighbour into the xmm0 register
	jmp ret_neighbours ; return neighbours

fourth: ; if current cell is on the upper bound 
    mov r12d, [int_max] ; move max value to the r12d register - it will mark it as invalid neighbour
	pinsrd xmm0, r12d, 2 ; insert the third neighbour into the xmm0 register
	mov r12d, [current] ; move current to r12d register
	add r12,40 ; add 40 to the current index - bottom neighbour
	pinsrd xmm0, r12d, 3 ; insert the fourth neighbour into the xmm0 register
	jmp ret_neighbours ; return neighbours

ret_neighbours:		
	endm

; finds the element with the smallest distance value

; modified registers: 
; eax - contains index of element with the smallest distance when the macro is finished
; r11d - stores pointer to the array with distances
; r12d - counter
; r14d - current min value
; r13d - stores pointer to the visited array
minimal_distance macro reg 
	mov r11d, [distances] ; move the pointer to the array with distances to r11d register
	mov r14d, [int_max] ; move the default max value to r14d register
	mov [min_value], r14d ; set the current min value to the max value
	mov [min_index], -1 ; set the current index of the cell with min value to -1
	mov r12d, -1  ; set the counter to -1 

minimal_loop: ; loop through the all elements of the array 
	inc r12d ; increment the counter 
	cmp r12d, [len] ; check if all elements of the array were checked 
	je return_minimum ; if yes return the index of the min element

	mov r13d, visited ; move the pointer to the array with visited elements to r13d array 
	cmp DWORD PTR[r13d + r12d*4], 0 ; check if current element was visited 
	JNE minimal_loop_2 ; if yes skip this element

	mov r14d, [min_value]  ; move the current min value to r14d register
	cmp [r11d], r14d ; check if the distance of the current element is smaller than the current minimum
	JAE minimal_loop_2 ; if no skip 

change_minimum: ; if the current element distance value is smaller - change the current minimum
	mov r14d, [r11d] ; move the current min value to the r14d register
	mov [min_value], r14d ; update the current min value
	mov [min_index], r12d ; update the current index of the element with min value

minimal_loop_2: ; proceed to the next element 
	add r11d, 4 ; move the pointer to the next element in the array 
	jmp minimal_loop ; jump to the loop - check nex element

return_minimum: ; minimum was found 
	mov eax, [min_index] ; store the index of the element with the smallest value in the eax register 
	endm

.data
len DD 0 ; holds the length of the array
dest DD 0 ; index of the destination cell 
source DD 0 ; index of the source cell
distances DD 0 ; holds array of distances 
visited DD 0 ; holds array of boolean values
previous DD 0 ; holds array of indexes to previous cells
int_max DD 268435455 ; max value 
min_value DD 4294967295 ; holds current min value - used in finding the minimum of the array
min_index DD -1 ; holds index of the current min value
current DD 0 ; index of the current cell 
.code

; fills arrays with default values, uses SIMD instructions

; input:
; r11d - pointer to the array that will be filled with default values
; xmm0 - default values 

; modified registers: 
; r11d - stores pointer to the array and the values of elements in that array are modified
; ecx - counter
initialize_arrays: 
		mov ecx, 25	; loop has to run exactly 25 times - counter
	do_loop: ; loads default values to the array
		movups [r11d    ], xmm0 ; moves default values stored in xmm0 register to 4 elements from the array
		movups [r11d+16 ], xmm0 ; moves default values stored in xmm0 register to 4 elements from the array
		movups [r11d+32 ], xmm0 ; moves default values stored in xmm0 register to 4 elements from the array
		movups [r11d+48 ], xmm0 ; moves default values stored in xmm0 register to 4 elements from the array
		movups [r11d+64 ], xmm0 ; moves default values stored in xmm0 register to 4 elements from the array
		movups [r11d+80 ], xmm0 ; moves default values stored in xmm0 register to 4 elements from the array
		movups [r11d+96 ], xmm0 ; moves default values stored in xmm0 register to 4 elements from the array
		movups [r11d+112], xmm0 ; moves default values stored in xmm0 register to 4 elements from the array

		add r11d, 128	;  move the pointer 32 elements 
		loop do_loop	; loop as long as ecx is not equal to 0
		ret ; return


; main procedure of the algorithm

; input: 
; ecx - pointer to the array with distances, int[800]
; edx - pointer to the visited cells array, int[800]
; r8 - pointer to the previous element array, int[800]
; r9 - index of the source cell, int 
; DWORD PTR[rsp + 48] - index of the destination cell, int 
; DWORD PTR[rsp + 56]  - length of the array, int 

; output: arrays passed as parameters are filled with values calculated by the algorithm

; modified registers: none
dijkstraASM proc

initialize: ; initialize the variables and arrays

	push rsi ; push the register source index value onto the stack


	mov [distances], ecx ; store the pointer to the array with distances (first parameter) in the variable
	mov [visited], edx ; store the pointer to the visited cells array (second parameter) in the variable
	mov rax, r8  ; move the pointer to the previous cells array to the rax register (third parameter)
	mov [previous], eax ; store the previous elements array in the variable
	mov rax, r9  ; move the index of the source cell to the rax register (fourth parameter)
	mov [source], eax ; store the source cell index in the variable
    mov eax, DWORD PTR[rsp + 48] ; take the index of the destination cell from the stack (fifth parameter)
	mov [dest], eax ; store the index of the destination cell in the variable
	mov eax, DWORD PTR[rsp + 56] ; take the length of the array from the stack (sixth parameter)
	mov [len], eax ; store the length in the variable

	; initialize the arrays 

	mov r11d, [distances] ; copy the pointer to the array of distances to the r11d register
	movups xmm0, [int_max] ; move the default max value to the xmm0 register
	shufps  xmm0, xmm0, 0 ; fill the xmm0 register with the default value (clone already loaded element 3 times)
	call initialize_arrays ; call the initize_arrays in order to fill the distances array with default (max) value
	mov r11d, [previous] ; copy the pointer to the array of previous elements to the r11d register 
	movups xmm0, [min_index] ; move the default -1 index value to the xmm0 register
	shufps  xmm0, xmm0, 0 ; fill the xmm0 register with the default value (clone already loaded element 3 times)
	call initialize_arrays ; call the initize_arrays in order to fill the previous elements array with default (-1) value

	mov r11d, [distances] ; move the pointer to the distances array to r11d register

	mov DWORD PTR[r11d+r9d*4], 0 ; set the distance of the source cell to zero

main_loop: ; main loop of the algorithm
	minimal_distance ; find the unvisited element with the minimum distance value and store its index in eax
	mov [current], eax ; store the index of the returned current cell to the variable


	cmp eax, [dest] ; if the current element is our destination - finish
	JE finish ; jump to the finish 


	mov r13d, visited 	; move the pointer to the visited array to r13d register
	mov DWORD PTR[r13d + eax*4], 1 ; mark current node as visited

	get_neighbours ; find and validiate the neighbours of the current cell

	mov r12d, -1 ; prepare the counter in r12d register


check_loop: ; check if the distance from the current element to its neighbours is smaller than their current distances, if yes - update the distance and set current cell as previous

	inc r12d ; increment the counter 
	cmp r12d, 4 ; check if all neighbours were check
	je main_loop ; if yes come back to the main loop and find new current cell

	pextrd r14d, xmm0, 0 ; extract the first element from the xmm0 register to r14d register
	psrldq xmm0, 4 ; shift xmm0 right by 4 bytes - second element becomes first 

	cmp r14d, 800 ; check if the index of the neighbour is valid - if yes it means that neighbour exist and can be checked
	jae skip ; if not skip this one

	mov r13d, visited ; move the pointer to the visited array to r13d register
	cmp DWORD PTR DWORD PTR[r13d + r14d*4], 1 ; check if the current neighbour was visited
	je skip ; if yes skip this one 

	mov r13d, distances ; move the pointer to the array of distances to r13d register
	mov r11d, [current] ; move the index of the current cell to r11d register

	mov eax,  DWORD PTR[r13d + r11d*4] ; store the distance to the current cell in the eax
	add eax, 1 ; distance to check = distances[current] + 1;


	mov r11d, DWORD PTR[r13d + r14d*4] 	; move the distance to the current neighbour to the r11d register 

	cmp eax, r11d	; compare if the from the current cell to the neighbour is smaller than neighbour's current distance
	jae skip ; if not skip 

	;if yes change the distance and previous element

	mov DWORD PTR[r13d + r14d*4], eax ; update the distance to the neigbhour - distance from current cell is smaller 
	mov r13d, [current] ; move the index of the current element to r13d register
	mov r11d, previous ; move the pointer to the array with previous cells to the r11d register 
	mov DWORD PTR[r11d + r14d*4], r13d		; set the current cell as the previous cell on the path to the neighbour
skip: 
	jmp check_loop ; proceed to the next neighbour

finish:  ; if the algorithm is finished
			pop rsi ; restore the value of the rsi register
	ret ; return from the procedure

dijkstraASM endp










END
