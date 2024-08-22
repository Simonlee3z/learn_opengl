#version 330 core
in vec3 Normal;
in vec3 fragPos;
in vec2 TexCoords;

out vec4 fragColor;

uniform vec3 viewPos;

struct Material{
    sampler2D diffuse;
    sampler2D specular;
    sampler2D emissive;
    float shininess;
};

struct Light{
    vec3 position;
    vec3 direction;
    float cutOff;
    float outercutOff;
    
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;

    float constant;
    float linear;
    float quadratic;
};

uniform Material material;
uniform Light light;

void main()
{
    // attenuation coefficient
    float distance = length(light.position - fragPos);
    float attenuation = 1.0 / (light.constant + light.linear * distance + light.quadratic * (distance * distance));

    vec3 lightDir = normalize(light.position - fragPos);
    float theta = dot(lightDir, normalize(-light.direction));
    float epsilon = light.cutOff - light .outercutOff;
    float intensity = clamp((theta - light.outercutOff) / epsilon, 0.0, 1.0);
    // ambient
    vec3 ambient = light.ambient * vec3(texture(material.diffuse, TexCoords));

    // diffuse
    vec3 norm = normalize(Normal);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = light.diffuse * vec3(texture(material.diffuse, TexCoords)) * diff;

    // specular
    vec3 viewDir = normalize(viewPos - fragPos);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 specular = spec * light.specular * vec3(texture(material.specular, TexCoords));

    // emissive
    // vec3 emissive = vec3(texture(material.emissive, TexCoords));

    diffuse *= attenuation * intensity;
    specular *= attenuation * intensity;
    vec3 result = ambient + diffuse + specular;
    fragColor = vec4(result, 1.0);
}
