ALTER TABLE `eom_1floor`.`model_data`   
	ADD COLUMN `order_start` CHAR(10) NULL AFTER `eo_type`;

ALTER TABLE `eom_1floor`.`alram_data`   
	ADD COLUMN `order_start` CHAR(10) NULL AFTER `write_time`;
