CREATE function [dbo].[FetchRawMaterialsStock] ( 
	@RawMaterialId int
)

returns float

as

begin

	declare @Result	float;

	with Facts as (
		select	  RawMaterials.Id [RMID]
				, RawMaterials.Code [RMCODE]
				, RawMaterialReceived.QuantityReceived
				, RawMaterialReceived.QuantityUsedThisLot
				, coalesce(
					(	select	ConversionRate 
						from	UnitOfMeasureConversion 
						where	UoMID2 = RawMaterials.uomid 
						and		UoMID1 = RawMaterialReceived.UOMID 
					), 1.0 ) [Conversion]
		from	RawMaterials
				join RawMaterialReceived on RawMaterials.ID = RawMaterialReceived.RawMaterialID
		where	RawMaterials.Id = @RawMaterialId
	), AdjustedFacts as ( 
			select  RMID,
					QuantityReceived * Conversion [AdjustedReceived],
					QuantityUsedThisLot * Conversion [AdjustedUsed]
			from	Facts
	)

	select	@Result = sum(AdjustedReceived - AdjustedUsed)
	from	AdjustedFacts;
	
	return coalesce(@Result, 0.0);

end