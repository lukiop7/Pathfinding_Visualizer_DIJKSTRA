
get_neighbours macro reg
	mov r12d, [current] ; current do r12d
	mov r14d, 0
	mov ecx, 40
	CDQ
	IDIV ecx

	mov r12d, edx
	cmp r12d, 0
	JE first

	cmp r12d, 39
	JE second 

	mov r12d, [current] ; current do r12d
	add r12d, 1
	mov [neighbours], r12d ; 
	add r14d, 4
	sub r12d, 2
	mov [neighbours +4], r12d
	add r14d, 4
	jmp part_2
first:
    mov r12d, [int_max]
	mov [neighbours ], r12d
	add r14d, 4
	mov r12d, [current] ; current do r12d
	add r12d,1
	mov r13d, [visited]
	mov [neighbours + 4], r12d
	add r14d, 4
	jmp part_2

second:
    mov r12d, [int_max]
	mov [neighbours], r12d
	add r14d, 4
	mov r12d, [current] ; current do r12d
	sub r12d,1
	mov r13d, [visited]
	mov [neighbours + 4], r12d
	add r14d, 4
	jmp part_2

part_2:
		mov r12d, [current] ; current do r12d
		add r12, 40 
		cmp r12, 800
		JAE third

		mov r12d, [current] ; current do r12d
		sub r12, 40 
		cmp r12, 800
		JAE fourth

		mov [neighbours + 8], r12d
		add r14d, 4
		add r12, 80 
		mov [neighbours + 12], r12d
		add r14d, 4
		jmp ret_neighbours
third:
    mov r12d, [int_max]
	mov [neighbours + 8], r12d
	add r14d, 4
	mov r12d, [current] ; current do r12d
	sub r12d,40
	mov r13d, [visited]

	mov [neighbours + 12], r12d
	add r14d, 4
	jmp ret_neighbours

fourth:
    mov r12d, [int_max]
	mov [neighbours + 8], r12d
	add r14d, 4
	mov r12d, [current] ; current do r12d
	add r12,40
	mov r13d, [visited]
	mov [neighbours + 12], r12d
	add r14d, 4
	jmp ret_neighbours

ret_neighbours:		
	endm

minimal_distance macro reg
	mov r12d, [len]
	mov r11d, [distances]
	mov r14d, [int_max]
	mov [min_value], r14d
	mov [min_index], -1
	mov r12d, -1

minimal_loop:
	inc r12d
	cmp r12d, [len]
	je return_minimum

	mov r13d, visited
	cmp DWORD PTR[r13d + r12d*4], 0
	JNE minimal_loop_2

	mov r14d, [min_value] 
	cmp [r11d], r14d
	JAE minimal_loop_2

change_minimum:
	mov r14d, [r11d]
	mov [min_value], r14d
	mov [min_index], r12d

minimal_loop_2:
	add r11d, 4
	jmp minimal_loop

return_minimum:
	mov eax, [min_index]
	endm

.data
len DD 0
dest DD 0 
source DD 0
distances DD 0
visited DD 0
previous DD 0
int_max DD 268435455
min_value DD 4294967295
min_index DD -1
current DD 0
neighbours DD 1, 1, 1, 1
.code

initialize_arrays:
		mov ecx, 25	; 
	do_loop:
		movups [r11d    ], xmm0
		movups [r11d+16 ], xmm0
		movups [r11d+32 ], xmm0
		movups [r11d+48 ], xmm0
		movups [r11d+64 ], xmm0
		movups [r11d+80 ], xmm0
		movups [r11d+96 ], xmm0
		movups [r11d+112], xmm0

		add r11d, 128	; 
		loop do_loop	; 
		ret

dijkstraASM proc

initialize:
	push rsi

	;save the parameters
	mov [distances], ecx
	mov [visited], edx
	mov rax, r8 
	mov [previous], eax
	mov rax, r9 
	mov [source], eax
	mov rax, 0
    mov eax, DWORD PTR[rsp + 48]
	mov [dest], eax
	mov eax, DWORD PTR[rsp + 56]
	mov [len], eax
	mov eax, 0

	;initialize the arrays 
	mov r11d, [distances]
	movups xmm0, [int_max]
	shufps  xmm0, xmm0, 0 
	call initialize_arrays
	mov r11d, [previous]
	movups xmm0, [min_index]
	shufps  xmm0, xmm0, 0 
	call initialize_arrays
	mov r11d, [distances]

	; set the source distance to 0 
	mov DWORD PTR[r11d+r9d*4], 0

main_loop:
	;find the current min
	minimal_distance
	mov [current], eax

	; if the current element is our destination - finish
	cmp eax, [dest]
	JE finish

	;mark current node as visited
	mov r13d, visited
	mov DWORD PTR[r13d + eax*4], 1

	;get its neighbours
	get_neighbours

	mov r12d, -1

	; put the neighbours on the stack
	movups xmm0, [neighbours]

check_loop: 
	inc r12d
	cmp r12d, 4
	je main_loop

	pextrd r14d, xmm0, 0
	psrldq xmm0, 4

	cmp r14d, 800
	jae skip

	mov r13d, visited
	cmp DWORD PTR DWORD PTR[r13d + r14d*4], 1
	je skip

	mov r13d, distances
	mov r11d, [current]

	;distance to check = distances[current] + 1;
	mov eax,  DWORD PTR[r13d + r11d*4]
	add eax, 1

	;current distance to r11d
	mov r11d, DWORD PTR[r13d + r14d*4]

	;compare if the new distance is smaller
	cmp eax, r11d
	jae skip

	;if yes change the distance and previous node
	mov DWORD PTR[r13d + r14d*4], eax ;distance
	mov r13d, [current]
	mov r11d, previous
	mov DWORD PTR[r11d + r14d*4], r13d		; previous
skip: 
	jmp check_loop
finish: 
			pop rsi
	ret

dijkstraASM endp










END
