+ Avaliação Final 41SCJ - Integration e Development Tools
+ 41SCJ / INTEGRATIONS & DEVELOPMENT TOOLS / RAFAEL THOMAZELLI MAZZUCATO

# Kafka_POC


API em **.Net 6** com exemplos de implementação de um Producer e um Consumer no **Apache Kafka**, utilizando a biblioteca **Confluent**.

A Api possui dois BackgroundServices:

**ConsumerBackgroundService**
  - Responsavel por plugar um consumidor no tópico especificado na propriedade Kafka:Topic presente no appsettings.json.
 
**ProducerBackgroundService**
  - Responsavel por gerar dados aleatórios e publicar no tópico especificado na propriedade Kafka:Topic presente no appsettings.json
  

# Como rodar o projeto ?

- Primeiro é preciso rodar o docker-compose.yml para subir uma instancia local do Kafka. 
  - Vai subir um container, com a imagem do zookepper e o kafka.
- O Kafka será exposto no localhost na porta 29092. A api já está apontando para essa porta na propriedade Kafka:Server, caso seja alterada a porta na qual o kafka está exposto, lembrar de atualizar o appsettings.json
- Com o container de pé, basta rodar o projeto localmente que a Api irá subir mostrando os dois endpoints disponiveis via Swagger.
  - O método **[POST]/Drone** permite realizar um POST com as propriedades do objeto Drone customizados.
  - O método **[POST]/Drone/Lista** permite realizar um Post com uma lista de objetos Drone customizados.
  
- **AutoProducerIsActive** é uma flag presente no AppSettings.json que quando **true**, irá ativar o ProducerBackgroundService que irá gerar objetos drones aleatórios.
- **QuantityMock** é a quantidade de objetos que serão geradas aleatóriamente pelo ProducerBackgroundService.
- **SecondsBetweenExecution** é o periodo entre cada execução do ProducerBackgroundService.
 
 # Prints do funcionamento da API:

![image](https://user-images.githubusercontent.com/43277888/178128594-b6b28515-7c10-466e-86e5-ce43d6362b18.png)
> Console com os logs iniciais do Startup da API, com a primeira execução do ProducerBackgroundService

![image](https://user-images.githubusercontent.com/43277888/178128616-71cbfc6c-c425-415c-854e-a1d4e61f0fef.png)
> Swagger com os métodos disponiveis. Foi inserido um drone com propriedades customizadas.

![image](https://user-images.githubusercontent.com/43277888/178128629-caae5913-6715-46a0-b2d3-ef284ae6c03e.png)
> Console com os logs demonstrando que o dado foi publicado e consumido com sucesso.
