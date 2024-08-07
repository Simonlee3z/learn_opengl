#version 330 core
out vec4 FragColor;
in vec2 ourTexCoord;
uniform sampler2D texture0;
uniform sampler2D texture1;
void main()
{
    // FragColor = vec4(ourColor, 1.0f);
    if(texture(texture1, ourTexCoord).a < 0.1)
    {
        FragColor = texture(texture0, ourTexCoord) * vec4(0.8, 0.8, 0.8, 1);
        // discard;
    else{
        FragColor = mix(texture(texture0, ourTexCoord), texture(texture1, ourTexCoord), 0.2);
    }
}