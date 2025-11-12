Клонировать репозиторий.
```sh
git clone https://github.com/artem777764/MicroserviceOrderProject
````

Для запуска проекта в среде разработки:

Первый терминал (Пользователи):
````sh
cd src/UserService
dotnet run
````
Второй терминал (Заказы):
````sh
cd src/OrderService
dotnet run
````
Третий терминал (Шлюз):
````sh
cd src/ApiGatewayService
dotnet run
````

Для запуска в среде продакшена:

Принять файлы кибернетиса:
````sh
cd k8s
kubectl apply -f api-gateway-deployment.yaml
kubectl apply -f ingress-service.yaml
kubectl apply -f order-deployment.yaml
kubectl apply -f postgres-order-deployment.yaml
kubectl apply -f postgres-user-deployment.yaml
kubectl apply -f secrets.yaml
kubectl apply -f user-deployment.yaml
````
