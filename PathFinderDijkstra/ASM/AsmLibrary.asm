.data
.code

sumArray proc
	mov eax, 0
repeat_count:
	dec r8d
	cmp r8d, -1
	je return_result
	add [rcx], r8d
	mov r11d, [rcx]
	add eax, r11d
	mov r11d, [rdx]
	add rcx, 4
	add rdx, 4
	jmp repeat_count
return_result:
	ret
sumArray endp

END
