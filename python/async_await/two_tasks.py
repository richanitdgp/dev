import asyncio

async def fetch():
    print('Starting...')
    asyncio.sleep(2)
    print('done...')
    return {'Happy Sunday'}

async def print_nums():
    for i in range(5):
        print(i)
        await asyncio.sleep(1)

# future - value that will be available in the future
async def main():
    task1 = asyncio.create_task(fetch())
    task2 = asyncio.create_task(print_nums())

    value = await task1
    print(value)
    await task2

asyncio.run(main())