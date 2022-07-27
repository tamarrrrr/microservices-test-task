# Тестовое задание по курсу Microservices

![Build Workflow](../../workflows/Build%20project/badge.svg?branch=master)

## Hello, world сервер

Написать простейший веб-сервер, который на _любой запрос_ возвращает строку `Hello, $GREETING_NAME`, где `GREETING_NAME`
– переменная среды (env).

### Требования

1. Сервер запускается на порту `8080`.
2. Нужно описать [Dockerfile](Dockerfile) для упаковки сервера в контейнер.
3. При старте контейнера через переменную среды `GREETING_NAME` передается имя.
4. В файле [build.yml](.github/workflows/build.yml) дописать сборку проекта с
   помощью [Github Actions](https://docs.github.com/en/actions).

Пример реализации [Microservices Test Task](https://github.com/Romanow/microservices-test-task-completed).

### Пояснения

```shell
# собираем проект
$ ./gradlew clean build
BUILD SUCCESSFUL in 2s
7 actionable tasks: 7 executed

# прописываем GREETING_NAME и запускаем локально
$ export GREETING_NAME=Alex   
$ ./gradlew bootRun
INFO 38015 --- [           main] r.r.microservices.SimpleApplicationKt    : Starting SimpleApplicationKt using Java 11.0.2 on aromanov.local with PID 38015 (/Users/aromanov/Develop/Projects/microservices/microservices-test-task/build/classes/kotlin/main started by aromanov in /Users/aromanov/Develop/Projects/microservices/microservices-test-task)
INFO 38015 --- [           main] r.r.microservices.SimpleApplicationKt    : No active profile set, falling back to 1 default profile: "default"
INFO 38015 --- [           main] o.s.b.w.embedded.tomcat.TomcatWebServer  : Tomcat initialized with port(s): 8080 (http)
INFO 38015 --- [           main] o.apache.catalina.core.StandardService   : Starting service [Tomcat]
INFO 38015 --- [           main] org.apache.catalina.core.StandardEngine  : Starting Servlet engine: [Apache Tomcat/9.0.63]
INFO 38015 --- [           main] o.a.c.c.C.[Tomcat].[localhost].[/]       : Initializing Spring embedded WebApplicationContext
INFO 38015 --- [           main] w.s.c.ServletWebServerApplicationContext : Root WebApplicationContext: initialization completed in 589 ms
INFO 38015 --- [           main] o.s.b.w.embedded.tomcat.TomcatWebServer  : Tomcat started on port(s): 8080 (http) with context path ''
INFO 38015 --- [           main] r.r.microservices.SimpleApplicationKt    : Started SimpleApplicationKt in 1.278 seconds (JVM running for 1.512)
INFO 38015 --- [nio-8080-exec-1] o.a.c.c.C.[Tomcat].[localhost].[/]       : Initializing Spring DispatcherServlet 'dispatcherServlet'
INFO 38015 --- [nio-8080-exec-1] o.s.web.servlet.DispatcherServlet        : Initializing Servlet 'dispatcherServlet'
INFO 38015 --- [nio-8080-exec-1] o.s.web.servlet.DispatcherServlet        : Completed initialization in 1 ms

# проверяем
$ curl http://localhost:8080
Hello, Alex

# собираем в docker
$ docker build . -t simple-server:1.0

# запускаем собранный образ
$ docker run -d \
    -p 8080:8080 \
    -e GREETING_NAME=Max \
    simple-server:1.0
    
# проверяем
$ curl http://localhost:8080
Hello, Max
```

### Проверка

Для проверки используется Github Actions, шаги сборки описаны в [build.yml](.github/workflows/build.yml).

1. Создаете fork этого репозитория.
2. Реализуете простейший web сервер по описанному заданию.
3. После успешного прохождения тестов в _вашем_ репозитории (вкладка Actions), создаете Pull Request в основной
   репозиторий (Pull requests -> New pull request), в названии Pull Request указываете **ваше имя**, (например _Романов
   Алексей_).