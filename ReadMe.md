# Portainer

<p><span style="font-weight:bold">=></span>
    Portainer is a Management UI that allows us to manage our Docker containers. The Portainer used to use docker with an interface. So, management is can be easily.
</p>
<br/>

> [Portainer Documents](https://docs.portainer.io/v/ce-2.11/start/install/server/docker/linux)

<br/>

## Deployment <hr/>

1) Open Windows PowerShell and enter codes :

```
docker volume create portainer_data
```
```
docker run -d -p 8000:8000 -p 9000:9000 --name portainer --restart=always -v /var/run/docker.sock:/var/run/docker.sock -v portainer_data:/data portainer/portainer-ce:2.11.1
```

<p><span style="color:yellow;font-weight:bold">Note:</span> Configure Port : 9000</p>
 
 ## Logging <hr/>

[localhost:9000](http://localhost:9000)

 <img src="https://user-images.githubusercontent.com/1864183/49685525-e8cc7100-fae4-11e8-9172-0f46d1dc0bfe.png">