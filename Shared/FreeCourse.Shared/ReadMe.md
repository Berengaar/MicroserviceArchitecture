# **Shared**
### Description :

<p style="color:gray">
<b><span style="color:aliceblue">=></span> This layer is a middleware for common objects in the project. I used  .Net Core (3.1) for this layer. Because the identity library we will use for 5.0 is paid. But for 3.1 is free. Also if you want you can use Core 5.0 with identity library's old versions but it can create some problems. I preferred like this.
</p>
<hr>

### Framework Reference :
<p style="color:gray">
<span style="color:aliceblue">=></span> We should edit file on shared project for csproj file. I did like this because
I applied base controller for status codes. And this project does not contain Controller or ControllerBase references. So we can add reference project like this. I added this tag between the project tags.
</p>
<p style="color:blue;">

	<ItemGroup>
		<FrameworkReference Include="Microsoft.AspNetCore.App"/>
	</ItemGroup>
</p>
<hr>

### Patterns :
* Static Factory Method Pattern
* Generic Response Pattern