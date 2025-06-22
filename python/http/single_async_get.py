import http.client
import asyncio
import aiohttp

# Need to install aiohttp before runnin this
# pip install aiohttp

async def fetch(url):
    async with aiohttp.ClientSession() as session:
        async with session.get(url) as response:
            return await response.text()

async def main():
    html = await fetch("https://microsoft.com")
    print(html)

asyncio.run(main())