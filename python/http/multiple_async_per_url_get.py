import http.client
import asyncio
import time
import aiohttp

# Need to install aiohttp before runnin this
# pip install aiohttp

# Simple fetch(url) with single get call for each url
async def fetch(url):
    async with aiohttp.ClientSession() as session:
        async with session.get(url) as response:
            return await response.text()

async def main():
    urls = [
        "https://microsoft.com",
        "https://apple.com",
        "https://google.com",
        "https://facebook.com"
    ]

    start_time = time.time()
    
    for url in urls:
        status = await fetch(url)
        print(status)

    elapsed_time = time.time() - start_time

    print(f"\n✅ Total time taken: {elapsed_time:.2f} seconds")

asyncio.run(main())