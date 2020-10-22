.data
len DD 0
dest DD 0 
source DD 0
distances DD 0
visited DD 0
previous DD 0
.code

sumArray proc
	mov [distances], ecx
	mov [visited], edx
	mov rax, r8 
	mov [previous], eax
	mov rax, r9 
	mov [source], eax
	mov rax, 0
    mov eax, DWORD PTR[rsp + 40]
	mov [dest], eax
	mov eax, DWORD PTR[rsp + 48]
	mov [len], eax
	mov eax, 0
repeat_count:
	dec [len]
	cmp [len], -1
	je return_result
	mov r11d, [r8]
	add eax, r11d
	mov r11d, [previous]
	add eax, [r11d]
	add r8, 4
	add [previous], 4
	jmp repeat_count
return_result:
	ret
sumArray endp

END
