.data
len DD 0
dest DD 0 
source DD 0
distances DD 0
visited DD 0
previous DD 0
int_max DD 4294967295
min_value DD 4294967295
min_index DD -1
current DD 0
neighbours DD 1, 1, 1, 1
neighbours_check DD 1, -1, 40, -40
neighbours_valid DD 4294967295,4294967295,4294967295,4294967295
.code

dijkstraAsm proc

initialize:
	push rsi
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

main_loop:
	call minimal_distance
	mov [current], eax
	cmp eax, [dest]
	JE finish
	mov r13d, visited
	mov DWORD PTR[r13d + eax*4], 1
	call get_neighbours
	movups xmm1, [neighbours_valid]
	movups xmm0, [neighbours]
    movups [neighbours],xmm0 
	mov r12d, -1
	mov r14d, [neighbours]
	push r14
	mov r14d, [neighbours+4]
	push r14
	mov r14d, [neighbours+8]
	push r14
	mov r14d, [neighbours+12]
	push r14
check_loop:
	inc r12d
	cmp r12d, 4
	je main_loop
	pop r14
	cmp r14, 800
	jae skip
	mov r13d, visited
	cmp DWORD PTR DWORD PTR[r13d + r14d*4], 1
	je skip
	mov r13d, distances
	mov r11d, [current]
	mov eax,  DWORD PTR[r13d + r11d*4]
	add eax, 1
	mov r11d, DWORD PTR[r13d + r14d*4]
	cmp eax, r11d
	jae skip
	mov DWORD PTR[r13d + r14d*4], eax ;distance
	mov r13d, [current]
	mov r11d, previous
	mov DWORD PTR[r11d + r14d*4], r13d		; previous
skip: 
	jmp check_loop
finish: 
	pop rsi
	ret
dijkstraAsm endp

minimal_distance:
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
	ret

get_neighbours:
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
	add r12d, 1
	mov [neighbours], r12d ; nie dziala musisz to przemyslec bardzo dobrze
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
	cmp DWORD PTR[r13d + r12d*4], 0
	JNE first_visited
	mov [neighbours + 4], r12d
	add r14d, 4
	jmp part_2
first_visited:
    mov r12d, [int_max]
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
	cmp DWORD PTR[r13d + r12d*4], 0
	JNE second_visited
	mov [neighbours + 4], r12d
	add r14d, 4
	jmp part_2
second_visited:
    mov r12d, [int_max]
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
	cmp DWORD PTR[r13d + r12d*4], 0
	JNE third_visited
	mov [neighbours + 12], r12d
	add r14d, 4
	jmp ret_neighbours
third_visited:
    mov r12d, [int_max]
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
	cmp DWORD PTR[r13d + r12d*4], 0
	JNE fourth_visited
	mov [neighbours + 12], r12d
	add r14d, 4
	jmp ret_neighbours
fourth_visited:
    mov r12d, [int_max]
	mov [neighbours + 12], r12d
	add r14d, 4
	jmp ret_neighbours
ret_neighbours:		
	ret





END
