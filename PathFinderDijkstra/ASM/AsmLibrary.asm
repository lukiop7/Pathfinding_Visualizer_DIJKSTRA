
.data
.code

asmAddTwoDoubles proc
	vaddpd ymm0, ymm0, ymm1
	ret
asmAddTwoDoubles endp


func proc a:DWORD, b:DWORD, c:DWORD, len:DWORD

  mov eax, len
  test eax, eax
  jnz @f
  ret

    @@:

  push ebx
  push esi

  xor eax, eax

  mov esi, a
  mov ebx, b
  mov ecx, c

    @@:

  mov edx, dword ptr ds:[ebx+eax*4]
  add edx, dword ptr ds:[ecx+eax*4]
  mov [esi+eax*4], edx
  cmp eax, len
  jl @b

  pop esi
  pop ebx

  ret  

func endp

END

