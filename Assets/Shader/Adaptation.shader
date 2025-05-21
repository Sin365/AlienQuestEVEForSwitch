Shader "Hidden/Contrast Stretch Adaptation" {
Properties {
 _MainTex ("Base (RGB)", 2D) = "white" {}
 _CurTex ("Base (RGB)", 2D) = "white" {}
}
SubShader { 
 Pass {
  ZTest Always
  ZWrite Off
  Cull Off
  Fog { Mode Off }
Program "vp" {
SubProgram "opengl " {
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
"!!ARBvp1.0
PARAM c[9] = { { 0 },
		state.matrix.mvp,
		state.matrix.texture[0] };
TEMP R0;
MOV R0.zw, c[0].x;
MOV R0.xy, vertex.texcoord[0];
DP4 result.texcoord[0].y, R0, c[6];
DP4 result.texcoord[0].x, R0, c[5];
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
END
# 8 instructions, 1 R-regs
"
}
SubProgram "d3d9 " {
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Matrix 4 [glstate_matrix_texture0]
"vs_2_0
def c8, 0.00000000, 0, 0, 0
dcl_position0 v0
dcl_texcoord0 v1
mov r0.zw, c8.x
mov r0.xy, v1
dp4 oT0.y, r0, c5
dp4 oT0.x, r0, c4
dp4 oPos.w, v0, c3
dp4 oPos.z, v0, c2
dp4 oPos.y, v0, c1
dp4 oPos.x, v0, c0
"
}
SubProgram "d3d11 " {
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
ConstBuffer "UnityPerDraw" 336
Matrix 0 [glstate_matrix_mvp]
ConstBuffer "UnityPerDrawTexMatrices" 768
Matrix 512 [glstate_matrix_texture0]
BindCB  "UnityPerDraw" 0
BindCB  "UnityPerDrawTexMatrices" 1
"vs_4_0
eefiecedeedelkdobbmimfefjdhgabnhlefmpcmlabaaaaaaciacaaaaadaaaaaa
cmaaaaaaiaaaaaaaniaaaaaaejfdeheoemaaaaaaacaaaaaaaiaaaaaadiaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaaebaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaafaepfdejfeejepeoaafeeffiedepepfceeaaklkl
epfdeheofaaaaaaaacaaaaaaaiaaaaaadiaaaaaaaaaaaaaaabaaaaaaadaaaaaa
aaaaaaaaapaaaaaaeeaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaaadamaaaa
fdfgfpfaepfdejfeejepeoaafeeffiedepepfceeaaklklklfdeieefceiabaaaa
eaaaabaafcaaaaaafjaaaaaeegiocaaaaaaaaaaaaeaaaaaafjaaaaaeegiocaaa
abaaaaaaccaaaaaafpaaaaadpcbabaaaaaaaaaaafpaaaaaddcbabaaaabaaaaaa
ghaaaaaepccabaaaaaaaaaaaabaaaaaagfaaaaaddccabaaaabaaaaaagiaaaaac
abaaaaaadiaaaaaipcaabaaaaaaaaaaafgbfbaaaaaaaaaaaegiocaaaaaaaaaaa
abaaaaaadcaaaaakpcaabaaaaaaaaaaaegiocaaaaaaaaaaaaaaaaaaaagbabaaa
aaaaaaaaegaobaaaaaaaaaaadcaaaaakpcaabaaaaaaaaaaaegiocaaaaaaaaaaa
acaaaaaakgbkbaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaakpccabaaaaaaaaaaa
egiocaaaaaaaaaaaadaaaaaapgbpbaaaaaaaaaaaegaobaaaaaaaaaaadiaaaaai
dcaabaaaaaaaaaaafgbfbaaaabaaaaaaegiacaaaabaaaaaacbaaaaaadcaaaaak
dccabaaaabaaaaaaegiacaaaabaaaaaacaaaaaaaagbabaaaabaaaaaaegaabaaa
aaaaaaaadoaaaaab"
}
SubProgram "d3d11_9x " {
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
ConstBuffer "UnityPerDraw" 336
Matrix 0 [glstate_matrix_mvp]
ConstBuffer "UnityPerDrawTexMatrices" 768
Matrix 512 [glstate_matrix_texture0]
BindCB  "UnityPerDraw" 0
BindCB  "UnityPerDrawTexMatrices" 1
"vs_4_0_level_9_1
eefieceddhnbicbokkmhnihbiniipgnpnicndjjjabaaaaaaceadaaaaaeaaaaaa
daaaaaaaciabaaaahiacaaaammacaaaaebgpgodjpaaaaaaapaaaaaaaaaacpopp
laaaaaaaeaaaaaaaacaaceaaaaaadmaaaaaadmaaaaaaceaaabaadmaaaaaaaaaa
aeaaabaaaaaaaaaaabaacaaaacaaafaaaaaaaaaaaaaaaaaaaaacpoppbpaaaaac
afaaaaiaaaaaapjabpaaaaacafaaabiaabaaapjaafaaaaadaaaaadiaabaaffja
agaaoekaaeaaaaaeaaaaadoaafaaoekaabaaaajaaaaaoeiaafaaaaadaaaaapia
aaaaffjaacaaoekaaeaaaaaeaaaaapiaabaaoekaaaaaaajaaaaaoeiaaeaaaaae
aaaaapiaadaaoekaaaaakkjaaaaaoeiaaeaaaaaeaaaaapiaaeaaoekaaaaappja
aaaaoeiaaeaaaaaeaaaaadmaaaaappiaaaaaoekaaaaaoeiaabaaaaacaaaaamma
aaaaoeiappppaaaafdeieefceiabaaaaeaaaabaafcaaaaaafjaaaaaeegiocaaa
aaaaaaaaaeaaaaaafjaaaaaeegiocaaaabaaaaaaccaaaaaafpaaaaadpcbabaaa
aaaaaaaafpaaaaaddcbabaaaabaaaaaaghaaaaaepccabaaaaaaaaaaaabaaaaaa
gfaaaaaddccabaaaabaaaaaagiaaaaacabaaaaaadiaaaaaipcaabaaaaaaaaaaa
fgbfbaaaaaaaaaaaegiocaaaaaaaaaaaabaaaaaadcaaaaakpcaabaaaaaaaaaaa
egiocaaaaaaaaaaaaaaaaaaaagbabaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaak
pcaabaaaaaaaaaaaegiocaaaaaaaaaaaacaaaaaakgbkbaaaaaaaaaaaegaobaaa
aaaaaaaadcaaaaakpccabaaaaaaaaaaaegiocaaaaaaaaaaaadaaaaaapgbpbaaa
aaaaaaaaegaobaaaaaaaaaaadiaaaaaidcaabaaaaaaaaaaafgbfbaaaabaaaaaa
egiacaaaabaaaaaacbaaaaaadcaaaaakdccabaaaabaaaaaaegiacaaaabaaaaaa
caaaaaaaagbabaaaabaaaaaaegaabaaaaaaaaaaadoaaaaabejfdeheoemaaaaaa
acaaaaaaaiaaaaaadiaaaaaaaaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaa
ebaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaaadadaaaafaepfdejfeejepeo
aafeeffiedepepfceeaaklklepfdeheofaaaaaaaacaaaaaaaiaaaaaadiaaaaaa
aaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaaeeaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadamaaaafdfgfpfaepfdejfeejepeoaafeeffiedepepfcee
aaklklkl"
}
}
Program "fp" {
SubProgram "opengl " {
Vector 0 [_AdaptParams]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_CurTex] 2D 1
"!!ARBfp1.0
OPTION ARB_precision_hint_fastest;
PARAM c[2] = { program.local[0],
		{ 0, 0.0039215689, 0.0099999998 } };
TEMP R0;
TEMP R1;
TEX R1.xy, fragment.texcoord[0], texture[1], 2D;
TEX R0.xy, fragment.texcoord[0], texture[0], 2D;
ADD R0.zw, -R0.xyxy, R1.xyxy;
MUL R0.zw, R0, c[0].x;
ABS R1.y, R0.z;
SLT R1.x, R0.z, c[1];
SLT R0.z, c[1].x, R0;
ADD R1.x, R0.z, -R1;
MAX R0.z, R1.y, c[1].y;
MUL R0.z, R1.x, R0;
ABS R1.y, R0.w;
SLT R1.x, R0.w, c[1];
SLT R0.w, c[1].x, R0;
MAX R1.y, R1, c[1];
ADD R0.w, R0, -R1.x;
MUL R0.w, R0, R1.y;
ADD R0.xy, R0, R0.zwzw;
MIN R0.y, R0, c[0];
MAX R0.x, R0, c[0].z;
ADD R0.z, -R0.y, R0.x;
ADD R0.z, R0, c[1];
RCP R0.w, R0.z;
MUL R0.w, R0.y, R0;
MOV result.color, R0;
END
# 24 instructions, 2 R-regs
"
}
SubProgram "d3d9 " {
Vector 0 [_AdaptParams]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_CurTex] 2D 1
"ps_2_0
dcl_2d s0
dcl_2d s1
def c1, 0.00000000, 1.00000000, 0.00392157, 0.01000000
dcl t0.xy
texld r3, t0, s0
texld r0, t0, s1
add r0.xy, -r3, r0
mul r2.xy, r0, c0.x
abs r0.x, r2
cmp r1.x, r2, c1, c1.y
cmp r2.x, -r2, c1, c1.y
add r1.x, r2, -r1
max r2.x, r0, c1.z
mul r4.x, r1, r2
abs r0.x, r2.y
max r0.x, r0, c1.z
cmp r2.x, -r2.y, c1, c1.y
cmp r1.x, r2.y, c1, c1.y
add r1.x, r2, -r1
mul r4.y, r1.x, r0.x
add r0.xy, r3, r4
max r3.x, r0, c0.z
min r0.x, r0.y, c0.y
add r1.x, -r0, r3
add r1.x, r1, c1.w
rcp r2.x, r1.x
mul r3.w, r0.x, r2.x
mov r3.y, r0.x
mov r3.z, r1.x
mov oC0, r3
"
}
SubProgram "d3d11 " {
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_CurTex] 2D 1
ConstBuffer "$Globals" 32
Vector 16 [_AdaptParams]
BindCB  "$Globals" 0
"ps_4_0
eefiecedbjfollgglgmmemfplidljafhmoebdkobabaaaaaadmadaaaaadaaaaaa
cmaaaaaaieaaaaaaliaaaaaaejfdeheofaaaaaaaacaaaaaaaiaaaaaadiaaaaaa
aaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaaeeaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaafdfgfpfaepfdejfeejepeoaafeeffiedepepfcee
aaklklklepfdeheocmaaaaaaabaaaaaaaiaaaaaacaaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaaaaaaaaaapaaaaaafdfgfpfegbhcghgfheaaklklfdeieefchmacaaaa
eaaaaaaajpaaaaaafjaaaaaeegiocaaaaaaaaaaaacaaaaaafkaaaaadaagabaaa
aaaaaaaafkaaaaadaagabaaaabaaaaaafibiaaaeaahabaaaaaaaaaaaffffaaaa
fibiaaaeaahabaaaabaaaaaaffffaaaagcbaaaaddcbabaaaabaaaaaagfaaaaad
pccabaaaaaaaaaaagiaaaaacacaaaaaaefaaaaajpcaabaaaaaaaaaaaegbabaaa
abaaaaaaeghobaaaabaaaaaaaagabaaaabaaaaaaefaaaaajpcaabaaaabaaaaaa
egbabaaaabaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaaaaaaaaaidcaabaaa
aaaaaaaaegaabaaaaaaaaaaaegaabaiaebaaaaaaabaaaaaadiaaaaaidcaabaaa
aaaaaaaaegaabaaaaaaaaaaaagiacaaaaaaaaaaaabaaaaaadbaaaaakmcaabaaa
aaaaaaaaaceaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaagaebaaaaaaaaaaa
dbaaaaakmcaabaaaabaaaaaaagaebaaaaaaaaaaaaceaaaaaaaaaaaaaaaaaaaaa
aaaaaaaaaaaaaaaadeaaaaaldcaabaaaaaaaaaaaegaabaiaibaaaaaaaaaaaaaa
aceaaaaaibiaiadlibiaiadlaaaaaaaaaaaaaaaaboaaaaaimcaabaaaaaaaaaaa
kgaobaiaebaaaaaaaaaaaaaakgaobaaaabaaaaaaclaaaaafmcaabaaaaaaaaaaa
kgaobaaaaaaaaaaadcaaaaajdcaabaaaaaaaaaaaogakbaaaaaaaaaaaegaabaaa
aaaaaaaaegaabaaaabaaaaaadeaaaaaibcaabaaaabaaaaaaakaabaaaaaaaaaaa
ckiacaaaaaaaaaaaabaaaaaaddaaaaaiccaabaaaabaaaaaabkaabaaaaaaaaaaa
bkiacaaaaaaaaaaaabaaaaaaaaaaaaaibcaabaaaaaaaaaaabkaabaiaebaaaaaa
abaaaaaaakaabaaaabaaaaaaaaaaaaahecaabaaaabaaaaaaakaabaaaaaaaaaaa
abeaaaaaaknhcddmdgaaaaafhccabaaaaaaaaaaaegacbaaaabaaaaaaaoaaaaah
iccabaaaaaaaaaaabkaabaaaabaaaaaackaabaaaabaaaaaadoaaaaab"
}
SubProgram "d3d11_9x " {
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_CurTex] 2D 1
ConstBuffer "$Globals" 32
Vector 16 [_AdaptParams]
BindCB  "$Globals" 0
"ps_4_0_level_9_1
eefiecedcpmmbidpceoepfifekppgafhjkfnjnjbabaaaaaafmafaaaaaeaaaaaa
daaaaaaaemacaaaanaaeaaaaciafaaaaebgpgodjbeacaaaabeacaaaaaaacpppp
nmabaaaadiaaaaaaabaacmaaaaaadiaaaaaadiaaacaaceaaaaaadiaaaaaaaaaa
abababaaaaaaabaaabaaaaaaaaaaaaaaaaacppppfbaaaaafabaaapkaibiaiadl
aknhcddmaaaaaaaaaaaaaaaafbaaaaafacaaapkaaaaaaaaaaaaaiadpaaaaaaia
aaaaialpbpaaaaacaaaaaaiaaaaacdlabpaaaaacaaaaaajaaaaiapkabpaaaaac
aaaaaajaabaiapkaecaaaaadaaaaapiaaaaaoelaabaioekaecaaaaadabaaapia
aaaaoelaaaaioekaacaaaaadaaaaadiaaaaaoeiaabaaoeibafaaaaadaaaaadia
aaaaoeiaaaaaaakafiaaaaaeaaaaaeiaaaaaaaibacaaaakaacaaffkafiaaaaae
aaaaaiiaaaaaaaiaacaakkkaacaappkaacaaaaadaaaaaeiaaaaappiaaaaakkia
cdaaaaacaaaaabiaaaaaaaiaalaaaaadabaaaeiaabaaaakaaaaaaaiaafaaaaad
acaaabiaaaaakkiaabaakkiafiaaaaaeaaaaabiaaaaaffibacaaaakaacaaffka
fiaaaaaeaaaaaeiaaaaaffiaacaakkkaacaappkacdaaaaacaaaaaciaaaaaffia
alaaaaadabaaaeiaabaaaakaaaaaffiaacaaaaadabaaaiiaaaaakkiaaaaaaaia
afaaaaadacaaaciaabaakkiaabaappiaacaaaaadaaaaadiaabaaoeiaacaaoeia
alaaaaadabaaabiaaaaaaaiaaaaakkkaakaaaaadabaaaciaaaaaffkaaaaaffia
acaaaaadaaaaabiaabaaffibabaaaaiaacaaaaadabaaaeiaaaaaaaiaabaaffka
agaaaaacaaaaabiaabaakkiaafaaaaadabaaaiiaaaaaaaiaabaaffiaabaaaaac
aaaiapiaabaaoeiappppaaaafdeieefchmacaaaaeaaaaaaajpaaaaaafjaaaaae
egiocaaaaaaaaaaaacaaaaaafkaaaaadaagabaaaaaaaaaaafkaaaaadaagabaaa
abaaaaaafibiaaaeaahabaaaaaaaaaaaffffaaaafibiaaaeaahabaaaabaaaaaa
ffffaaaagcbaaaaddcbabaaaabaaaaaagfaaaaadpccabaaaaaaaaaaagiaaaaac
acaaaaaaefaaaaajpcaabaaaaaaaaaaaegbabaaaabaaaaaaeghobaaaabaaaaaa
aagabaaaabaaaaaaefaaaaajpcaabaaaabaaaaaaegbabaaaabaaaaaaeghobaaa
aaaaaaaaaagabaaaaaaaaaaaaaaaaaaidcaabaaaaaaaaaaaegaabaaaaaaaaaaa
egaabaiaebaaaaaaabaaaaaadiaaaaaidcaabaaaaaaaaaaaegaabaaaaaaaaaaa
agiacaaaaaaaaaaaabaaaaaadbaaaaakmcaabaaaaaaaaaaaaceaaaaaaaaaaaaa
aaaaaaaaaaaaaaaaaaaaaaaaagaebaaaaaaaaaaadbaaaaakmcaabaaaabaaaaaa
agaebaaaaaaaaaaaaceaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaadeaaaaal
dcaabaaaaaaaaaaaegaabaiaibaaaaaaaaaaaaaaaceaaaaaibiaiadlibiaiadl
aaaaaaaaaaaaaaaaboaaaaaimcaabaaaaaaaaaaakgaobaiaebaaaaaaaaaaaaaa
kgaobaaaabaaaaaaclaaaaafmcaabaaaaaaaaaaakgaobaaaaaaaaaaadcaaaaaj
dcaabaaaaaaaaaaaogakbaaaaaaaaaaaegaabaaaaaaaaaaaegaabaaaabaaaaaa
deaaaaaibcaabaaaabaaaaaaakaabaaaaaaaaaaackiacaaaaaaaaaaaabaaaaaa
ddaaaaaiccaabaaaabaaaaaabkaabaaaaaaaaaaabkiacaaaaaaaaaaaabaaaaaa
aaaaaaaibcaabaaaaaaaaaaabkaabaiaebaaaaaaabaaaaaaakaabaaaabaaaaaa
aaaaaaahecaabaaaabaaaaaaakaabaaaaaaaaaaaabeaaaaaaknhcddmdgaaaaaf
hccabaaaaaaaaaaaegacbaaaabaaaaaaaoaaaaahiccabaaaaaaaaaaabkaabaaa
abaaaaaackaabaaaabaaaaaadoaaaaabejfdeheofaaaaaaaacaaaaaaaiaaaaaa
diaaaaaaaaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaaeeaaaaaaaaaaaaaa
aaaaaaaaadaaaaaaabaaaaaaadadaaaafdfgfpfaepfdejfeejepeoaafeeffied
epepfceeaaklklklepfdeheocmaaaaaaabaaaaaaaiaaaaaacaaaaaaaaaaaaaaa
aaaaaaaaadaaaaaaaaaaaaaaapaaaaaafdfgfpfegbhcghgfheaaklkl"
}
}
 }
}
Fallback Off
}