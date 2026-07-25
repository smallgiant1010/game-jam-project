using Godot;
using System;
using System.Collections.Generic;


public partial class SpawnManager : Node2D
{
	public static SpawnManager Instance { private set; get; }
	private enum Entity
	{
		RAT,
		KID,
		ELDERLY,
		KAREN,
		THIEF,
	}

	private record EntityDetails(Entity Name, int Weight);
	[Export] private int spawnProbability = 100;
	public int customerSpawnProbability = 70;
	private List<List<EntityDetails>> entityProbabilitiesPerDay;
	private double spawnDelay = 5f;
	[Export] private Timer timer_;
	private bool spawning = false;

	public override void _Ready()
	{
		entityProbabilitiesPerDay = new(7);
		InitializeProbabilitiesList();
		timer_.Timeout += OnTimeout;
		GameManager.Instance.StartGame += OnStartGame;
		GameManager.Instance.EndGame += OnEndGame;
	}

	private void OnStartGame()
	{
		timer_.Start(spawnDelay);
		spawning = true;
	}
	
	private void OnEndGame()
	{
		spawning = false;
		spawnProbability = 100;
	}

    private void InitializeProbabilitiesList()
    {
		entityProbabilitiesPerDay[(int)Day.SUNDAY] =
		[
			new EntityDetails(Entity.RAT, 0),
			new EntityDetails(Entity.KID, 50),
			new EntityDetails(Entity.ELDERLY, 50),
			new EntityDetails(Entity.KAREN, 0),
			new EntityDetails(Entity.THIEF, 0)
		];

		entityProbabilitiesPerDay[(int)Day.MONDAY] =
		[
			new EntityDetails(Entity.RAT, 10),
			new EntityDetails(Entity.KID, 40),
			new EntityDetails(Entity.ELDERLY, 40),
			new EntityDetails(Entity.KAREN, 10),
			new EntityDetails(Entity.THIEF, 0)
		];

		entityProbabilitiesPerDay[(int)Day.TUESDAY] =
		[
			new EntityDetails(Entity.RAT, 5),
			new EntityDetails(Entity.KID, 15),
			new EntityDetails(Entity.ELDERLY, 40),
			new EntityDetails(Entity.KAREN, 15),
			new EntityDetails(Entity.THIEF, 5)
		];

		entityProbabilitiesPerDay[(int)Day.WEDNESDAY] =
		[
			new EntityDetails(Entity.RAT, 10),
			new EntityDetails(Entity.KID, 10),
			new EntityDetails(Entity.ELDERLY, 20),
			new EntityDetails(Entity.KAREN, 40),
			new EntityDetails(Entity.THIEF, 20)
		];

		entityProbabilitiesPerDay[(int)Day.THURSDAY] =
		[
			new EntityDetails(Entity.RAT, 5),
			new EntityDetails(Entity.KID, 5),
			new EntityDetails(Entity.ELDERLY, 10),
			new EntityDetails(Entity.KAREN, 40),
			new EntityDetails(Entity.THIEF, 40)
		];

		entityProbabilitiesPerDay[(int)Day.FRIDAY] =
		[
			new EntityDetails(Entity.RAT, 20),
			new EntityDetails(Entity.KID, 20),
			new EntityDetails(Entity.ELDERLY, 20),
			new EntityDetails(Entity.KAREN, 20),
			new EntityDetails(Entity.THIEF, 20)
		];
		
		entityProbabilitiesPerDay[(int)Day.SATURDAY] =
		[
			new EntityDetails(Entity.RAT, 20),
			new EntityDetails(Entity.KID, 10),
			new EntityDetails(Entity.ELDERLY, 10),
			new EntityDetails(Entity.KAREN, 40),
			new EntityDetails(Entity.THIEF, 20)
		];
    }

	private void OnTimeout()
	{
		if (Random.Shared.Next(100) <= spawnProbability)
		{
			int gachaForCustomer = Random.Shared.Next(100);
			int gachaForType = Random.Shared.Next(100);
			if (gachaForCustomer <= customerSpawnProbability)
			{
				if (gachaForType <= 50)
				{
					//spawn regular customer
				}
				else
				{
					//spawn customer with dog
				}
			}
			else
			{
				foreach (EntityDetails ED in entityProbabilitiesPerDay[(int)GameManager.Instance.currentDay])
				{
					if (gachaForType < ED.Weight)
					{
						// spawn enemy
						switch (ED.Name)
						{
							case Entity.ELDERLY:
								break;
							case Entity.KID:
								break;
							case Entity.KAREN:
								break;
							case Entity.THIEF:
								break;
							case Entity.RAT:
								break;
						}
					}
					gachaForCustomer -= ED.Weight;
				}
			}
		}

		if(spawning)
		{
			timer_.Start(spawnDelay);
		}
	}
	
	public void ChangeSpawnProbability(int probability)
	{
		spawnProbability *= probability;
	}

	public override void _Process(double delta)
	{
	}
}
