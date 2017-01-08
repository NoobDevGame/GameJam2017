#version 400
uniform mat4 WorldViewProj;

in uint inputData;
out vec2 psTexcoord;
flat out uint psTexIndex;

void main()
{
	const vec2[] uvLookup = vec2[4](vec2(0.0,0.0),vec2(0.0,1.0),vec2(1.0,0.0),vec2(1.0,1.0));
	

	vec4 position = vec4(float(inputData >> 24),float((inputData >> 16) & 0xFF),0.0,1.0);
	psTexIndex = (inputData>>8) & 0xFF;
	psTexcoord = uvLookup[(inputData) & 0x3];
	

	gl_Position = WorldViewProj*position;


}

